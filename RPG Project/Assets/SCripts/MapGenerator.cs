using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap, ColorMap,DrawMesh}

    public DrawMode drawMode;

    public Noise.NormalizeMode normalizeMode; 

    public const int mapChunkSize = 241;
    [Range(0,6)]
    public int editorLODPreview;
    
    [SerializeField] public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public int seed;
    public Vector2 offset;
    [SerializeField] public bool autoUpdate;
    [SerializeField] public float[,] noiseMap;
    [SerializeField] public List<float> noiseList;
    public TerrainType[] regions;

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQ = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQ = new Queue<MapThreadInfo<MeshData>>();
    [System.Serializable]
    public struct TerrainType {

        public string name;
        public float height;
        public Color color;
    
    
    }

    public struct MapData {

        public readonly float[,] heightMap;
        public readonly Color[] colorMap;

        public MapData(float[,] heightMap, Color[] colorMap) {

            this.heightMap = heightMap;
            this.colorMap = colorMap;
        
        }
    
    }

    public struct MapThreadInfo<T> {

        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback,T parameter) {

            this.callback = callback;
            this.parameter = parameter;
        }
    }
     MapData GenerateMapData(Vector2 center){
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, center + offset,normalizeMode);
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int x = 0; x < mapChunkSize; x++) {

            for (int y = 0; y < mapChunkSize; y++) {

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {

                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;

                        break;
                    }


                }
            
            }
        
        
        }


        return new MapData(noiseMap,colorMap);

    }

    public void DrawMapInEditor() {

        MapData mapData = GenerateMapData(Vector2.zero);
        MapDisplay display = GetComponent<MapDisplay>();
        noiseList = Noise.ReturnList(noiseMap);
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {

            display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize));

        }
        else if (drawMode == DrawMode.DrawMesh)
        {

            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorLODPreview), TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize));

        }

    }

    public void RequestMapData(Action<MapData> callback,Vector2 center) {

        ThreadStart threadStart = delegate
        {

            MapDataThread(callback,center);

        };

        new Thread(threadStart).Start();
    
    }

    public void RequestMeshData(MapData mapData,int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData, lod, callback);

        };

        new Thread(threadStart).Start();
        

    }

    void MapDataThread(Action<MapData> callback,Vector2 center) {

        MapData mapData = GenerateMapData(center);
        lock (mapDataThreadInfoQ)
        {
            mapDataThreadInfoQ.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    
    }

    void MeshDataThread(MapData mapData,int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod);
        lock (meshDataThreadInfoQ)
        {
            meshDataThreadInfoQ.Enqueue(new MapThreadInfo<MeshData>(callback,meshData));
        }

    }

    private void Update()
    {
        if (mapDataThreadInfoQ.Count > 0) {

            for (int i = 0; i < mapDataThreadInfoQ.Count;i++) {

                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQ.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            
            }
        
        }

        if (meshDataThreadInfoQ.Count > 0)
        {

            for (int i = 0; i < meshDataThreadInfoQ.Count; i++)
            {

                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQ.Dequeue();
                threadInfo.callback(threadInfo.parameter);

            }

        }
    }
    private void OnValidate()
    {
        

        if (lacunarity < 1) {

            lacunarity = 1;
        
        }

        if (octaves < 0) {

            octaves = 0;
        
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

public class EndlessTerrain : MonoBehaviour
{


    const float viewerMoveThreshold4ChunkUpdate = 25f;
    const float squareViewerMoveThreshold4ChunkUpdate = viewerMoveThreshold4ChunkUpdate * viewerMoveThreshold4ChunkUpdate;
    Vector2 viewrPositionOld;
    public Transform viewer;
    public static Vector2 viewerPos;
    static MapGenerator mapGenerator;
    int chunkSize;
    int chunksVisibleInViewDistance;
    Dictionary<Vector2, TerrainChunk> terrainChunkDict = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();
    public LODInfo[] detailLevels;
    public static float maxViewDistance;

    public Material mapMaterial;

    private void Start()
    {

        maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        mapGenerator = GetComponent<MapGenerator>();
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / chunkSize);
        UpdateVisibleChunks();
    }
    private void Update()
    {
        viewerPos = new Vector2(viewer.position.x, viewer.position.z);
        if ((viewrPositionOld - viewerPos).sqrMagnitude > squareViewerMoveThreshold4ChunkUpdate) {

            viewrPositionOld = viewerPos;
            UpdateVisibleChunks();
        }
        
    }
    void UpdateVisibleChunks()
    {

        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {

            terrainChunksVisibleLastUpdate[i].SetVisible(false);

        }
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoorX = Mathf.RoundToInt(viewerPos.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPos.y / chunkSize);


        for (int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++)
        {


            for (int xOffset = -chunksVisibleInViewDistance; xOffset <= chunksVisibleInViewDistance; xOffset++)
            {

                Vector2 viewedChunkCoord = new Vector2(currentChunkCoorX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunkDict.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDict[viewedChunkCoord].UpdateTerrainChunk();
                 
                }
                else
                {

                    terrainChunkDict.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize,detailLevels, transform,mapMaterial));

                }

            }

        }

    }


    public class TerrainChunk
    {

        GameObject meshObject;
        Vector2 position;

        Bounds bounds;

        MapData mapData;
        bool mapDataReceived;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        int prevLODIndex = -1;


        public TerrainChunk(Vector2 coordinate, int size,LODInfo[] detailLevels, Transform parent,Material material)
        {
            this.detailLevels = detailLevels;
            position = coordinate * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);
            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshObject.transform.position = positionV3;
            meshObject.transform.parent = parent;
            meshObject.transform.eulerAngles = new Vector3(meshObject.transform.eulerAngles.x, meshObject.transform.eulerAngles.y, meshObject.transform.eulerAngles.z + 180f);
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];

            for (int i = 0; i < detailLevels.Length; i++) {

                lodMeshes[i] = new LODMesh(detailLevels[i].lod,UpdateTerrainChunk);
            
            }
            
            mapGenerator.RequestMapData(OnMapDataReceived,position);
        }

        void OnMapDataReceived(MapData mapData) {

            this.mapData = mapData;
            mapDataReceived = true;
            Texture2D texture = TextureGenerator.TextureFromColorMap(mapData.colorMap,MapGenerator.mapChunkSize,MapGenerator.mapChunkSize);
            meshRenderer.material.mainTexture = texture;
            UpdateTerrainChunk();

        }

        void OnMeshDataReceived(MeshData meshData) {

            meshFilter.mesh = meshData.CreateMesh();
        
        
        }

        public void UpdateTerrainChunk()
        {


            if (mapDataReceived)
            {
                float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPos));

                bool visible = viewerDstFromNearestEdge <= maxViewDistance;

                if (visible)
                {

                    int lodIndex = 0;
                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {

                        if (viewerDstFromNearestEdge > detailLevels[i].visibleDistanceThreshold)
                        {

                            lodIndex = i + 1;

                        }

                        else break;


                        

                        

                        

                    }

                    if (lodIndex != prevLODIndex)
                    {

                        LODMesh lodMesh = lodMeshes[lodIndex];

                        if (lodMesh.hasMesh)
                        {
                            prevLODIndex = lodIndex;
                            meshFilter.mesh = lodMesh.mesh;

                        }
                        else if (!lodMesh.hasRequestMesh)
                        {

                            lodMesh.RequestMesh(mapData);

                        }
                    }
                    terrainChunksVisibleLastUpdate.Add(this);
                }
                SetVisible(visible);


            }




        }

        public void SetVisible(bool visible)
        {

            meshObject.SetActive(visible);
        }

        public bool IsVisible() {

            return meshObject.activeSelf;
        }

    }


    class LODMesh {

        public Mesh mesh;
        public bool hasRequestMesh;
        public bool hasMesh;
        int lod;
        System.Action updateCallback;

        public LODMesh(int lod,System.Action updateCallback) {

            this.lod = lod;
            this.updateCallback = updateCallback;
        
        }

        void OnMeshDataReceived(MeshData meshData) {

            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(MapData mapData) {

            hasRequestMesh = true;
            mapGenerator.RequestMeshData(mapData,lod,OnMeshDataReceived);
        
        
        }
    }

    [System.Serializable]
    public struct LODInfo{

        public int lod;
        public float visibleDistanceThreshold;
    
    }
}

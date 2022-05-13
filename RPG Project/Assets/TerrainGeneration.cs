using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainGeneration : MonoBehaviour
{
    private Terrain terrain;
    public int width = 256;
    public int height = 256;
    public int depth = 20;
    public float scale = 20f;

    public float offsetX;
    public float offsetY;

    private void Start()
    {

        offsetX = UnityEngine.Random.Range(0f,9999f);

        offsetY = UnityEngine.Random.Range(0f, 9999f);
        terrain =  GetComponent<Terrain>();

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0,0,GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width;x++) {

            for (int y = 0; y < height;y++) {

                heights[x, y] = CalculateHeights(x, y);
            
            }
        
        }

        return heights;
    }

    private float CalculateHeights(int x, int y)
    {
        float xcoords = (float)x / width * scale + offsetX;
        float ycoords = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xcoords,ycoords);
    }
}

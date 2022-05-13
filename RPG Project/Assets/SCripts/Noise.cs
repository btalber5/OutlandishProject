using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{

    public enum NormalizeMode { Local, Global};
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity,Vector2 offset,NormalizeMode normalizeMode) {

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0f;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves;i++) {

            float offSetX = prng.Next(-100000,100000) + offset.x;
            float offSetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offSetX, offSetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int x = 0; x < mapWidth; x++)
        {

            for (int y = 0; y < mapHeight; y++)
            {
                amplitude = 1;
                frequency = 1;

                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {

                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; ;
                    noiseMap[x, y] = perlinValue;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;

                    frequency *= lacunarity;

                }
                if (noiseHeight > maxLocalNoiseHeight) {

                    maxLocalNoiseHeight = noiseHeight;

                } else if (noiseHeight < minLocalNoiseHeight) {

                    minLocalNoiseHeight = noiseHeight;
                
                }


                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < mapWidth; x++)
        {

            for (int y = 0; y < mapHeight; y++)
            {
                if (normalizeMode == NormalizeMode.Local)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }
                else if(normalizeMode == NormalizeMode.Global) {

                    float normalizeHeight = (noiseMap[x, y] + maxPossibleHeight) / (2f * maxPossibleHeight);
                    noiseMap[x, y] = Mathf.Clamp(normalizeHeight,0,int.MaxValue);
                }
            
            }
        }


        return noiseMap;

    }

    public static List<float> ReturnList(float[,] listIn) {
        float width = listIn.GetLength(0);
        float height = listIn.GetLength(1);

        List<float> returnList = new List<float>();
        for (int x = 0; x < width; x++) {

            for (int y = 0; x < height; x++) {

                returnList.Add(listIn[x, y]);
            
            }
        
        }

        return returnList;
        
    
    }
}

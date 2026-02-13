using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    const float BIG = 100000f;
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, Vector2 original, float seed = 0f, float scale = 0.001f)
    {
        float[,] noiseMap = new float[mapHeight, mapWidth];

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float sampleX = (x + original.x + seed * 0.123f + BIG) * scale;
                float sampleY = (y + original.y + seed * 0.567f + BIG) * scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x,y] = perlinValue;
            }
        }

        return noiseMap;
    }
}

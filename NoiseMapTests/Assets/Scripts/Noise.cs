using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persist, float lacun, int offX, int offY)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0) //don’t divide by zero
        {
            scale = .00001f;
        }

        float minHT = -1;
        float maxHT = 1;

        for (int y = 0; y < mapHeight; y++)//y
        {
            for (int x = 0; x < mapWidth; x++)//x
            {
                float amp = 1;
                float freq = 1;
                float noiseHT = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float someX = x / scale * freq + offX;
                    float someY = y / scale * freq + offY;

                    float perlinVal = Mathf.PerlinNoise(someX, someY) *2 - 1;
                    noiseHT += perlinVal * amp;
                    amp *= persist;
                    freq *= lacun;
                }

                if(noiseHT > maxHT)
                {
                    maxHT = noiseHT;
                } else if(noiseHT < minHT)
                {
                    minHT = noiseHT;
                }
               
                noiseMap[x, y] = noiseHT;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minHT, maxHT, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

}

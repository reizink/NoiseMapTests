using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0) //don’t divide by zero
        {
            scale = .00001f;
        }

        for (int y = 0; y < mapHeight; y++)//y
        {
            for (int x = 0; x < mapWidth; x++)//x
            {
                float someX = x / scale;
                float someY = y / scale;

                float perlinVal = Mathf.PerlinNoise(someX, someY);
                noiseMap[x, y] = perlinVal;
            }
        }

        return noiseMap;
    }

}

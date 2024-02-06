using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator2 : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale; 
    public Renderer textRender;

    public bool auto;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        DrawNoiseMap(noiseMap);
    }


    public void DrawNoiseMap(float[,] noiseM)
    {
        int w = noiseM.GetLength(0); //width
        int h = noiseM.GetLength(1); //height
        Texture2D texture = new Texture2D(w, h);

        Color[] colorMap = new Color[w * h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                colorMap[y * w + x] = Color.Lerp(Color.black, Color.white, noiseM[x, y]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();
        textRender.sharedMaterial.mainTexture = texture;
        textRender.transform.localScale = new Vector3(w, 1, h);
    }

}

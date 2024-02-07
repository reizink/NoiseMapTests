using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator2 : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale; 
    public Renderer textRender;
     
    public int octaves;
    public float persist;
    public float lacun;

    public int OffsetX, OffsetY;

    public enum DrawMode
    {
        noiseMap, ColorMap, Mesh
    }
    public DrawMode drawMode;
    public bool auto;


    //added since class*************
    public float meshMultiplier;
    public Renderer textRenderer;
    public MeshRenderer meshRenderer;
    public MeshFilter meshfilter;
    public TerrainType[] sections;
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }


    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persist, lacun, OffsetX, OffsetY);


        //Added colorMap since class*********
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapWidth; y++)
        {
            for (int x = 0; x < mapHeight; x++)
            {
                float curHeight = noiseMap[x, y];
                for (int i = 0; i < sections.Length; i++)
                {
                    if(curHeight <= sections[i].height)
                    {
                        colorMap[y*mapWidth+x] = sections[i].color;
                        break;
                    }
                }
            }
        }

        //DrawNoiseMap(noiseMap); //// editted since class
        if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshMultiplier), TextureFromCMap(colorMap, mapWidth, mapHeight));
        } else if(drawMode == DrawMode.ColorMap)
        {
            DrawTexture(TextureFromCMap(colorMap, mapWidth, mapHeight));
        } else if(drawMode == DrawMode.noiseMap){
            DrawTexture(TextureFromHMap(noiseMap));
        }
    }

    public void DrawTexture(Texture2D texture)
    {
        textRender.sharedMaterial.mainTexture = texture;
        textRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public Texture2D TextureFromHMap(float[,] heightMap)
    {
        int w = heightMap.GetLength(0); //width
        int h = heightMap.GetLength(1); //height
        Texture2D texture = new Texture2D(w, h);

        Color[] colorMap = new Color[w * h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                colorMap[y * w + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TextureFromCMap(colorMap, w, h);
    }

    public Texture2D TextureFromCMap(Color[] colorMap, int w, int h)
    {
        Texture2D texture = new Texture2D(w, h);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }


    //added since classe *****
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshfilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

}

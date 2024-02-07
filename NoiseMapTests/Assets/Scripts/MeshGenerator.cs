using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float HMultiplier) //multiplier is what changes the height
    {
        int w = heightMap.GetLength(0);
        int h = heightMap.GetLength(1);
        MeshData meshData = new MeshData(w, h);
        int vertexIndex = 0;

        float topLX = (w - 1) / -2f;
        float topLZ = (h - 1) / 2f;

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLX + x, heightMap[x, y] * HMultiplier, topLZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x/(float)w, y/(float)h);

                if(x < w -1 && y < h - 1)
                {
                    meshData.AddTri(vertexIndex, vertexIndex + w + 1, vertexIndex + w);
                    meshData.AddTri(vertexIndex + w + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
    
}

public class MeshData
{
    public int[] triangles;
    public Vector3[] vertices;
    public Vector2[] uvs;
    private int triIndex;

    public MeshData(int meshW, int meshH)
    {
        vertices = new Vector3[meshW * meshH];
        uvs = new Vector2[meshW * meshH];
        triangles = new int[(meshW - 1) * (meshH - 1) * 6];
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
    
    public void AddTri(int i, int j, int k)
    {
        triangles[triIndex] = i;
        triangles[triIndex + 1] = j;
        triangles[triIndex + 2] = k;
        triIndex += 3;
    }
}
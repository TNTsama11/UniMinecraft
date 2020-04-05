using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> vertices = new List<Vector3>();//网格顶点信息
    public List<int> triangles = new List<int>();//网格三角面信息
    public List<Vector2> uv = new List<Vector2>();//网格UV信息
    public List<Vector3> colVertices = new List<Vector3>();//网格顶点碰撞信息
    public List<int> colTriangles = new List<int>();//网格三角面碰撞信息
    public bool useRenderDataForCol; //是否启用碰撞

    public MeshData()
    {

    }

    public void AddQuadTriangles()
    {
        triangles.Add(vertices.Count- 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (useRenderDataForCol)
        {
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 3);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 1);
        }
    }

    public void AddVeriex(Vector3 vertex)
    {
        vertices.Add(vertex);
        if (useRenderDataForCol)
        {
            colVertices.Add(vertex);
        }
    }

    public void AddTriangle(int tir)
    {
        triangles.Add(tir);
        if (useRenderDataForCol)
        {
            colTriangles.Add(tir - (vertices.Count - colVertices.Count));
        }
    }
    
}

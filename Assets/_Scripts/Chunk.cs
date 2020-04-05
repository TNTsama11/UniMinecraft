using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{

    public World world;
    public WorldPos pos;

    Block[,,] blocks;
    public static int chunkSize = 16;
    public bool update = true;

    MeshFilter filter;
    MeshCollider coll;


    private void Awake()
    {
        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();
        blocks = new Block[chunkSize, chunkSize, chunkSize];
    }

    void Update()
    {
        
    }

    public Block GetBlock(int x,int y,int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            return blocks[x, y, z];            
        }
        return new BlockAir();
    }

    public void UpdateChunk()
    {
        MeshData meshData = new MeshData();
        for(int x = 0; x < chunkSize; x++)
        {
            for(int y = 0; y < chunkSize; y++)
            {
                for(int z = 0; z < chunkSize; z++)
                {
                    meshData = blocks[x, y, z].BlockData(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }

    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();
        Mesh colMesh = new Mesh();
        coll.sharedMesh = colMesh;
        coll.sharedMesh.vertices = meshData.colVertices.ToArray();
        coll.sharedMesh.triangles = meshData.colTriangles.ToArray();
    }

    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
        {
            return false;
        }
        return true;
    }

    public void SetBlock(int x, int y,int z,Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
    }
}

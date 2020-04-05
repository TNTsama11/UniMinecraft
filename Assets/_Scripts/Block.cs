using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    north,
    east,
    south,
    west,
    up,
    down
}

public class Block 
{
    const float tileSize = 0.25f;

    const float shrinkSize = 0.001f;

    public Block()
    {

    }

    public struct Tile { public int x;public int y; }

    public virtual Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 0;
        tile.y = 0;
        return tile;
    }

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);
        UVs[0] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y)+new Vector2(shrinkSize, shrinkSize);
        UVs[1] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize) + new Vector2(-shrinkSize, shrinkSize);
        UVs[2] = new Vector2(tileSize * tilePos.x , tileSize * tilePos.y + tileSize) + new Vector2(-shrinkSize, -shrinkSize);
        UVs[3] = new Vector2(tileSize * tilePos.x , tileSize * tilePos.y) + new Vector2(shrinkSize, -shrinkSize);
        return UVs;
    }

    //检测方块周围是否有方块，并且剔除多余的面
    public virtual MeshData BlockData(Chunk chunk,int x,int y,int z,MeshData meshData)
    {
        meshData.useRenderDataForCol = true;
        //判断方块的顶上的方块是否有底面，没有就创建当前方块的顶面
        if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.down))
        {
            meshData=FaceDataUp(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.up))
        {
            meshData=FaceDataDown(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.south))
        {
            meshData=FaceDataNorth(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.north))
        {
            meshData=FaceDataSouth(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x+1, y, z ).IsSolid(Direction.west))
        {
            meshData=FaceDataEast(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.east))
        {
            meshData=FaceDataWest(chunk, x, y, z, meshData);
        }
        return meshData;
    }

    protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        //方块中心点是0.5
        // meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x +0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.up));
        return meshData;
    }

    protected virtual MeshData FaceDataDown(Chunk chunk,int x,int y,int z,MeshData meshData)
    {
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.down));
        return meshData;
    }

    protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.east));
        return meshData;
    }

    protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.north));
        return meshData;
    }

    protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.south));
        return meshData;
    }

    protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVeriex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.west));
        return meshData;
    }
    public virtual bool IsSolid(Direction direction)
    {
        switch (direction)
        {
            case Direction.north:
                return true;
            case Direction.east:
                return true;
            case Direction.south:
                return true;
            case Direction.west:
                return true;
            case Direction.up:
                return true;
            case Direction.down:
                return true;
        }
        return false;
    }
}

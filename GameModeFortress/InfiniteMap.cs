﻿using System;
using System.Collections.Generic;
using System.Text;
using ManicDigger;

namespace GameModeFortress
{
    public class InfiniteMapChunked : IMapStorage
    {
        [Inject]
        public IWorldGenerator generator { get; set; }
        byte[, ,][, ,] chunks;
        #region IMapStorage Members
        public int MapSizeX { get; set; }
        public int MapSizeY { get; set; }
        public int MapSizeZ { get; set; }
        public int GetBlock(int x, int y, int z)
        {
            byte[, ,] chunk = GetChunk(x, y, z);
            return chunk[x % chunksize, y % chunksize, z % chunksize];
        }
        public void SetBlock(int x, int y, int z, int tileType)
        {
            byte[, ,] chunk = GetChunk(x, y, z);
            chunk[x % chunksize, y % chunksize, z % chunksize] = (byte)tileType;
        }
        public float WaterLevel { get; set; }
        public void Dispose()
        {
        }
        public void UseMap(byte[, ,] map)
        {
        }
        #endregion
        public byte[, ,] GetChunk(int x, int y, int z)
        {
            x = x / chunksize;
            y = y / chunksize;
            z = z / chunksize;
            byte[, ,] chunk = chunks[x, y, z];
            if (chunk == null)
            {
                //byte[, ,] newchunk = new byte[chunksize, chunksize, chunksize];
                byte[, ,] newchunk = generator.GetChunk(x, y, z, chunksize);
                chunks[x, y, z] = newchunk;
                return newchunk;
            }
            return chunk;
        }
        public int chunksize = 16;
        public void Reset(int sizex, int sizey, int sizez)
        {
            MapSizeX = sizex;
            MapSizeY = sizey;
            MapSizeZ = sizez;
            chunks = new byte[sizex / chunksize, sizey / chunksize, sizez / chunksize][, ,];
        }
    }
}

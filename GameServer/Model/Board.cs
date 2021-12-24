using System;
using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class Board
    {
        public Chunk[] Chunks;
        private int _chunkNumber;
        public static int Width = 100;

        public Board()
        {
            CreateChunks();
        }

        private void CreateChunks()
        {
            _chunkNumber = (int)Math.Pow(Board.Width / Chunk.Width, 2);
            Chunks = new Chunk[_chunkNumber];

            for (int i = 0; i < _chunkNumber; i++)
            {
                Chunks[i] = new Chunk(i);
            }
        }

        public Entity[] GetEntitiesAround(int chunkId)
        {
            var entities = new List<Entity>();
            var chunks = GetConnectedChunks(chunkId);

            foreach (var chunk in chunks)
            {
                entities.AddRange(chunk.Entities);
            }

            return entities.ToArray();
        }

        private List<Chunk> GetConnectedChunks(int chunkId)
        {
            var chunks = new List<Chunk>();
            int chunksInRow = Board.Width / Chunk.Width;

            var ids = new int[]
            {
                chunkId + 1, chunkId - 1,
                chunkId + chunksInRow,
                chunkId - chunksInRow,
                chunkId + chunksInRow + 1,
                chunkId + chunksInRow - 1,
                chunkId - chunksInRow + 1,
                chunkId - chunksInRow - 1,
            };

            foreach (var id in ids)
            {
                if (IsChunkIdValid(id))
                {
                    chunks.Add(Chunks[id]);
                }
            }

            return chunks;
        }

        private bool IsChunkIdValid(int id)
        {
            if (id >= 0 && id < _chunkNumber)
            {
                return true;
            }
            return false;
        }

        public static bool IsPositionValid(Vector2 position)
        {
            if (position.X >= 0 && position.X <= Width &&
                position.Y >= 0 && position.Y <= Width)
            {
                return true;
            }
            return false;
        }

        public static int GetChunkIdByPosition(Vector2 position)
        {
            if (!IsPositionValid(position)) return -1;
            int h = (int)Math.Floor(position.X / Chunk.Width);
            int v = (int)Math.Floor(position.Y / Chunk.Width);
            int chunksInRow = Board.Width / Chunk.Width;
            int id = (v * chunksInRow) + h;
            return id;
        }

        public void UpdateChunksForEntity(Entity entity)
        {
            var currentChunkId = GetChunkIdByPosition(entity.Position);
            if (entity.ChunkId == currentChunkId || IsChunkIdValid(entity.ChunkId)) return;
            Chunks[currentChunkId].Entities.Add(entity);
            Chunks[entity.ChunkId].Entities.Remove(entity);
            entity.ChunkId = currentChunkId;
        }

        public void AddEntityToBoard(Entity entity)
        {
            var currentChunkId = GetChunkIdByPosition(entity.Position);
            Chunks[currentChunkId].Entities.Add(entity);
            entity.ChunkId = currentChunkId;
        }

        public void RemoveEntityFromBoard(Entity entity)
        {
            Chunks[entity.ChunkId].Entities.Remove(entity);
            entity.ChunkId = -1;
        }


    }
}

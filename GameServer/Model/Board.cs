using System;
using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class Board
    {
        #region Fields

        public Chunk[] Chunks;
        private int _chunkNumber;
        public const int Width = 100;

        #endregion Fields

        #region Contructor

        public Board()
        {
            CreateChunks();
        }

        #endregion Contructor

        #region Methods

        private void CreateChunks()
        {
            _chunkNumber = (int)Math.Pow(Width / Chunk.Width, 2);
            Chunks = new Chunk[_chunkNumber];

            for (var i = 0; i < _chunkNumber; i++)
            {
                Chunks[i] = new Chunk(i);
            }
        }

        public Entity[] GetEntitiesAround(int chunkId)
        {
            var entities = new List<Entity>();
            List<Chunk> chunks = GetConnectedChunks(chunkId);

            foreach (Chunk chunk in chunks)
            {
                entities.AddRange(chunk.Entities);
            }

            return entities.ToArray();
        }

        private List<Chunk> GetConnectedChunks(int chunkId)
        {
            var chunks = new List<Chunk>();
            int chunksInRow = Width / Chunk.Width;

            int[] ids = GetNearestChunksIds(chunkId, chunksInRow);

            foreach (int id in ids)
            {
                if (IsChunkIdValid(id))
                {
                    chunks.Add(Chunks[id]);
                }
            }

            return chunks;
        }

        private static int[] GetNearestChunksIds(int chunkId,
            int chunksInRow)
        {
            var ids = new int[]
            {
                chunkId,
                chunkId + 1, chunkId - 1,
                chunkId + chunksInRow,
                chunkId - chunksInRow,
                chunkId + chunksInRow + 1,
                chunkId + chunksInRow - 1,
                chunkId - chunksInRow + 1,
                chunkId - chunksInRow - 1,
            };

            return ids;
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
            if (!IsPositionValid(position))
            {
                return -1;
            }

            int horizontal = (int)Math.Floor(position.X / Chunk.Width);
            int vertical = (int)Math.Floor(position.Y / Chunk.Width);

            int chunksInRow = Width / Chunk.Width;
            int id = (vertical * chunksInRow) + horizontal;

            return id;
        }

        public void UpdateChunksForEntity(Entity entity)
        {
            var currentChunkId = GetChunkIdByPosition(entity.Position);

            if (entity.ChunkId == currentChunkId ||
                !IsChunkIdValid(entity.ChunkId) ||
                !IsChunkIdValid(currentChunkId)) 
            {
                return; 
            }

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

        #endregion Methods
    }
}

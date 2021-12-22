using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Agario.Model
{
    class Board
    {

        public Chunk[] Chunks;
        private int _chunkNumber;
        public static int Width = 100;

        public Board()
        {
            CreateChunks();
        }

        public void CreateChunks()
        {
            _chunkNumber = (int)Math.Pow(Board.Width / Chunk.Width, 2);
            Chunks = new Chunk[_chunkNumber];
            for (int i = 0; i < _chunkNumber; i++)
            {
                Chunks[i] = new Chunk(i);
            }
        }

        public List<Chunk> GetConnectedChunks(int chunkId)
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

        public void GetChunkByPosition(Vector2 position)
        {

        }


    }
}

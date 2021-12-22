using System.Collections.Generic;

namespace Agario.Model
{
    class Chunk
    {
        public int Id;
        public static int Width = 20;
        public List<Entity> Entities = new();

        public Chunk(int id)
        {
            Id = id;
        }


    }
}

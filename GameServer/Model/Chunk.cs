using System.Collections.Generic;

namespace Agario.Model
{
    public class Chunk
    {
        public int Id;
        public const int Width = 20;
        public List<Entity> Entities = new();

        public Chunk(int id)
        {
            Id = id;
        }
    }
}

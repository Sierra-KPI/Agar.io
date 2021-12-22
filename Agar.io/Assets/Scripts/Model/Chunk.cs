using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

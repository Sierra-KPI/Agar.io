using System.Collections.Generic;

namespace Agario.Model
{
    public class Chunk
    {
        #region Fields

        public int Id;
        public const int Width = 20;
        public List<Entity> Entities = new();

        #endregion Fields

        #region Contructor

        public Chunk(int id)
        {
            Id = id;
        }

        #endregion Constructor
    }
}

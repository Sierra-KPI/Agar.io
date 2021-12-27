using System.Numerics;

namespace Agario.Model
{
    public abstract class Entity
    {
        #region Fields

        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public EntityType EntityType { get; set; }
        public bool IsDead { get; set; }
        public int ChunkId { get; set; }

        #endregion Fields

        #region Methods

        public void Die()
        {
            IsDead = true;
            Radius = 0;
        }

        #endregion Methods
    }
}

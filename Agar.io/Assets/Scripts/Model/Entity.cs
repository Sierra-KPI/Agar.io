using System.Numerics;

namespace Agario.Model
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }
        public int Radius { get; set; }
        public EntityType EntityType { get; set; }
        public bool IsDead { get; set; }
        public int ChunkId { get; set; }
    }
}

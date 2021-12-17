using System.Numerics;

namespace Agario.Model
{

    public enum EntityType
    {
        Food,
        Blob
    }


    public abstract class Entity
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public EntityType EntityType { get; set; }
    }
}

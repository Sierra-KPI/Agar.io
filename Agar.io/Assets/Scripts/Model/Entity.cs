using System.Numerics;

namespace Agario.Model
{
    public abstract class Entity
    {
        #region Fields

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public EntityType EntityType { get; set; }
        public bool IsDead { get; set; }

        #endregion Fields
    }
}

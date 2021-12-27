using System.Numerics;

namespace Agario.Model
{
    public class Player : Entity
    {
        #region Fields

        private const float StartRadius = 3;
        public string Name { get; set; } = "Default name";
        private const float SpeedMultiplier = 0.4f;

        #endregion Fields

        #region Constructor

        public Player(Vector2 position)
        {
            Position = position;
            Radius = StartRadius;

            EntityType = EntityType.Player;
            ChunkId = Board.GetChunkIdByPosition(Position);
        }

        #endregion Constructor

        #region Methods

        public void Move(Vector2 direction)
        {
            if (Position.X > Board.Width ||
                Position.X < 0 ||
                Position.Y > Board.Width ||
                Position.Y < 0)
            {
                Die();
            }

            float maxSpeed = SpeedMultiplier / Radius;

            Vector2 Velocity = Vector2.Multiply(direction, maxSpeed);
            Position += Velocity;
        }

        public void Kill(Entity entity)
        {
            Radius += entity.Radius;
            entity.Die();
        }

        #endregion Methods
    }
}

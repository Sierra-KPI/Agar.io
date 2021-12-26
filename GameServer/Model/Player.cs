using System.Numerics;

namespace Agario.Model
{
    public class Player : Entity
    {
        private const float StartRadius = 3;
        public string Name { get; set; } = "Default name";

        public Player(Vector2 position)
        {
            EntityType = EntityType.Player;
            Radius = StartRadius;
            Position = position;
            ChunkId = Board.GetChunkIdByPosition(Position);
        }

        public void Move(Vector2 direction)
        {
            if (Position.X > Board.Width ||
                Position.X < 0 ||
                Position.Y > Board.Width ||
                Position.Y < 0)
            {
                Die();
            }

            float maxSpeed = 0.4f / Radius;

            Vector2 Velocity = Vector2.Multiply(direction, maxSpeed);
            Position += Velocity;
        }

        public void Kill(Entity entity)
        {
            Radius += entity.Radius;
            entity.Die();
        }
    }
}

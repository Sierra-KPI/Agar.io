using System.Numerics;

namespace Agario.Model
{
    public class Player : Entity
    {
        public static float startRadius = 3;
        public string Name { get; set; }

        public Player(Vector2 position)
        {
            EntityType = EntityType.Player;
            Radius = startRadius;
            Position = position;
            ChunkId = Board.GetChunkIdByPosition(Position);
        }

        public void Move(Vector2 direction)
        {
            float maxSpeed = 10 / Radius;

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

using System.Numerics;

namespace Agario.Model
{
    public class Food : Entity
    {

        public Food(Vector2 position)
        {
            Position = position;
            EntityType = EntityType.Food;
            Radius = 2;
            ChunkId = Board.GetChunkIdByPosition(Position);
        }

        public void Die()
        {
            // Die(
        }
    }
}

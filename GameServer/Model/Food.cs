using System.Numerics;

namespace Agario.Model
{
    public class Food : Entity
    {
        public Food(Vector2 position)
        {
            Id = 1000;
            Position = position;
            Radius = 2;

            EntityType = EntityType.Food;
            ChunkId = Board.GetChunkIdByPosition(Position);
        }
    }
}

using System.Numerics;

namespace Agario.Model
{
    public class Food
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public Food(Vector2 position)
        {
            Position = position;
        }

        public void Die()
        {
            // Die(
        }
    }
}

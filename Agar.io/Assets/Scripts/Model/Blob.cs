using System.Numerics;

namespace Agario.Model
{
    public class Blob
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public void Die()
        {
            // die(
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
            //Position += move towards the center
        }

        public void Combine()
        {
            // if it is colliding with another blob of this player
        }

        public void Split()
        {
            // on spacebar reduce its radius in half and add some
            // force towards mouse position
        }
    }
}

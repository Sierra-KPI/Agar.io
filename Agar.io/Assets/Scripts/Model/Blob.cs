using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class Blob
    {
        public Player Owner { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        // Need controller and probably Object Pooling
        public void Die()
        {
            // die(
        }

        // Need controller
        public void Move(Vector2 direction)
        {
            Position += direction;
            //Position += move towards the center
        }

        // Need controller
        public void Combine()
        {
            foreach (Blob blob in Owner.Blobs)
            {
                if (CollisionDetection.AreColliding(Position, blob.Position,
                    Radius, blob.Radius))
                {
                    Radius += blob.Radius;
                    Owner.Blobs.Remove(blob);
                }
            }
        }

        // Need controller
        public void Split()
        {
            // on spacebar reduce its radius in half and add some
            // force towards mouse position
        }
    }
}

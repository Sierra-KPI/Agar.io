using System.Numerics;

namespace Agario.Model
{
    public class Blob : Entity
    {
        public Player Owner { get; set; }

        public Blob()
        {
            EntityType = EntityType.Blob;
        }

        // Need controller and probably Object Pooling
        public void Die()
        {
            // die(
        }

        // Need controller
        public void Move(Vector2 direction)
        {
            Position += direction;
            Position += GetBlobsCenter();
        }

        private Vector2 GetBlobsCenter()
        {
            Vector2 center = new Vector2();
            int blobsNum = 0;

            foreach (Blob blob in Owner.Blobs)
            {
                center += blob.Position;
                blobsNum++;
            }

            center = new Vector2(center.X / blobsNum, center.Y / blobsNum);

            return center;
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

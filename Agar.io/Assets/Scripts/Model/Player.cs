using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class Player
    {
        public string Name { get; set; }
        public List<Blob> Blobs { get; set; }
        // Probably We Will get it with packages?
        public List<Blob> VisibleBlobs { get; set; }

        public void Die() { }

        public void Move(Vector2 direction)
        {
            foreach (Blob blob in Blobs)
            {
                blob.Move(direction);
            }
        }

        public float GetTotalRadius()
        {
            float totalRadius = 0f;

            foreach (Blob blob in Blobs)
            {
                totalRadius += blob.Radius;
            }

            return totalRadius;
        }
    }
}

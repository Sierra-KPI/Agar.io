using System.Numerics;

namespace Agario.Model
{
    public class Blob
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public void Die() { }
        public void Move() { }
        public void Combine() { }
        public void Split() { }
    }
}

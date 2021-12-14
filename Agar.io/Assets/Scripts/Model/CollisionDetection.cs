using System.Numerics;

namespace Agario.Model
{
    public static class CollisionDetection
    {
        public static bool AreColliding(Vector2 firstPosition,
            Vector2 secondPosition, float firstRadius, float secondRadius)
        {
            var radius = firstRadius + secondRadius;

            var deltaX = firstPosition.X - secondPosition.X;
            var deltaY = firstPosition.Y - secondPosition.Y;

            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }
    }
}

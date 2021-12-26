namespace Agario.Model
{
    public static class CollisionDetection
    {
        public static bool AreColliding(Entity firstEntity,
            Entity secondEntity)
        {
            var radius = firstEntity.Radius / 12 + secondEntity.Radius / 12;

            var deltaX = firstEntity.Position.X - secondEntity.Position.X;
            var deltaY = firstEntity.Position.Y - secondEntity.Position.Y;

            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }
    }
}

namespace Agario.Model
{
    public static class CollisionDetection
    {
        #region Method

        public static bool AreColliding(Entity firstEntity,
            Entity secondEntity)
        {
            float radius = firstEntity.Radius / 12 + secondEntity.Radius / 12;

            float deltaX = firstEntity.Position.X - secondEntity.Position.X;
            float deltaY = firstEntity.Position.Y - secondEntity.Position.Y;

            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }

        #endregion Method
    }
}

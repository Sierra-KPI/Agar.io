namespace Agario.Model
{
    public class Player : Entity
    {
        #region Fields

        public string Name { get; set; }
        public int Id { get; set; }

        private const int StartRadius = 3;

        #endregion Fields

        #region Constuctor

        public Player()
        {
            Radius = StartRadius;
            EntityType = EntityType.Player;
        }

        #endregion Constructor
    }
}

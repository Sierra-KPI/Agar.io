namespace Agario.Model
{
    public class Player : Entity
    {
        #region Fields

        public string Name { get; set; }
        public int Id { get; set; }

        #endregion Fields

        #region Constuctor

        public Player()
        {
            Radius = 3;
            EntityType = EntityType.Player;
        }

        #endregion Constructor
    }
}

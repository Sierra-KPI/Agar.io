namespace Agario.Model
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Player()
        {
            Radius = 3;
            EntityType = EntityType.Player;
        }

    }
}

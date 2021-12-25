namespace Agario.Model
{
    class Player : Entity
    {
        public string Name { get; set; }
        public Player()
        {
            EntityType = EntityType.Player;
        }

    }
}

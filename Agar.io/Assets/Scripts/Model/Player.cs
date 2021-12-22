using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class Player : Entity
    {
        public static int startRadius = 3;
        public string Name { get; set; }
        public void Die() { }

        public Player(string name)
        {
            EntityType = EntityType.Player;
            Radius = startRadius;
            Name = name;
        }

        public void Move(Vector2 direction)
        {

        }
    }
}

using System.Collections.Generic;

namespace Agario.Model
{
    public class Player
    {
        public string Name { get; set; }
        public List<Blob> Blobs {get; set;}

        public void Die() { }
    }
}

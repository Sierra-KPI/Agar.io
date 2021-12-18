using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class PacketHandler
    {
        public static void Connection(PacketBase _packet)
        {
            Console.WriteLine("Name: " + ((ConnectionRequestPacket)_packet).Name);

        }

        public static void PlayerPosition(PacketBase _packet)
        {
            var packet1 = ((PlayerPosition)_packet);
            Console.WriteLine("X: " + packet1.X + " Y: " + packet1.Y);
        }


    }
}

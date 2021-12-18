using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class PacketHandler
    {
        public static void GetConnectionRequest(Client client, PacketBase _packet)
        {
            var packet = (ConnectionRequestPacket)_packet;
            client.Name = packet.Name;

            Console.WriteLine("GetConnectionRequest -> Name: " + packet.Name);
            SendConnectionResponse(client);
        }

        public static void SendConnectionResponse(Client client)
        {
            var packet = new ConnectionResponsePacket
            {
                Type = PacketType.ConnectionResponse,
                ClientId = client.Id,
                PacketId = 0
            };

            Server.SendUDPData(client.EndPoint, packet);
            Console.WriteLine("SendConnectionResponse");
        }

        public static void GetPlayerPosition(Client client, PacketBase _packet)
        {
            var packet = ((PlayerPosition)_packet);
            Console.WriteLine("X: " + packet.X + " Y: " + packet.Y);
        }


    }
}

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
            client.ReceivePacketsCounter = _packet.PacketId;

            Console.WriteLine("GetConnectionRequest -> Name: " + packet.Name);
            SendConnectionResponse(client);
        }

        public static void SendConnectionResponse(Client client)
        {
            var packet = new ConnectionResponsePacket
            {
                Type = PacketType.ConnectionResponse,
                ClientId = client.Id,
                PacketId = ++client.SendPacketsCounter,
                ClientPacketId = client.ReceivePacketsCounter
            };

            Server.SendUDPData(client.EndPoint, packet);
            Console.WriteLine("SendConnectionResponse");
        }

        public static void GetPlayerPosition(Client client, PacketBase _packet)
        {
            var packet = ((PlayerPosition)_packet);

            if (client.ReceivePacketsCounter >= packet.PacketId)
            {
                return;
            }

            client.ReceivePacketsCounter = packet.PacketId;
            client.TimeOfLife = 0;

            // change player coordinates


            Console.WriteLine("X: " + packet.X + " Y: " + packet.Y);
        }

        public static void SendBoardUpdate(Client client)
        {
            // get array of players in board

            var packet = new BoardUpdatePacket
            {
                Type = PacketType.BoardUpdate,
                ClientId = client.Id,
                PacketId = ++client.SendPacketsCounter,
                ClientPacketId = client.ReceivePacketsCounter,
                PlayersNumber = 3,
            };

            

            Server.SendUDPData(client.EndPoint, packet);
            Console.WriteLine("SendBoardUpdate -> ClientId: " + client.Id);
        }


    }
}

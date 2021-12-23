using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameServer
{
    internal class PacketHandler
    {
        public static void GetConnectionRequest(Client client,
            PacketBase _packet)
        {
            var packet = (ConnectionRequestPacket)_packet;
            client.Player.Name = packet.Name;
            client.ReceivePacketsCounter = _packet.PacketId;

            Console.WriteLine("GetConnectionRequest -> Name: " +
                packet.Name);
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

            Server.SendUDPData(client, packet);
            Console.WriteLine("SendConnectionResponse");
        }

        public static void GetPlayerPosition(Client client,
            PacketBase _packet)
        {
            var packet = ((PlayerPosition)_packet);

            if (client.ReceivePacketsCounter >= packet.PacketId)
            {
                return;
            }

            client.ReceivePacketsCounter = packet.PacketId;
            client.TimeOfLife = 0;

            var direction = new Vector2(packet.X, packet.Y);
            client.Player.Move(direction);
            Server.Game.Board.UpdateChunksForEntity(client.Player);

            //Console.WriteLine("X: " + packet.X + " Y: " + packet.Y);
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

            Server.SendUDPData(client, packet);
            //Console.WriteLine("SendBoardUpdate -> ClientId: " + client.Id);
        }

        public static void GetPlayerInfoRequest(Client client,
           PacketBase _packet)
        {
            var packet = (PlayerInfoRequestPacket)_packet;
            client.ReceivePacketsCounter = _packet.PacketId;

            var player = Server.GetClient(packet.PlayerId);

            Console.WriteLine("GetPlayerInfoRequest");
            SendPlayerInfoResponse(client, player);
        }

        public static void SendPlayerInfoResponse(Client client, Client player)
        {
            var packet = new PlayerInfoResponsePacket
            {
                Type = PacketType.PlayerInfoResponse,
                ClientId = client.Id,
                PacketId = ++client.SendPacketsCounter,
                ClientPacketId = client.ReceivePacketsCounter,
                PlayerId = player.Id,
                Player = new PlayerInfoPacket
                {
                    Name = player.Player.Name,
                    Color = "color" // fix
                }
            };

            Server.SendUDPData(client, packet);
            Console.WriteLine("SendPlayerInfoResponse about: " + player.Player.Name);
        }

        public static void GetLeaderBoardRequest(Client client,
           PacketBase _packet)
        {
            var packet = (LeaderBoardRequestPacket)_packet;
            client.ReceivePacketsCounter = packet.PacketId;

            Console.WriteLine("GetLeaderBoardRequest");
            SendLeaderBoardResponse(client);
        }

        public static void SendLeaderBoardResponse(Client client)
        {
            var players = new List<PlayerInfoPacket>();
            foreach (var player in Server.Game.GetLeaderBoard())
            {
                players.Add(new PlayerInfoPacket
                {
                    Name = player.Name,
                    Color = "color", // fix
                    Size = player.Radius,
                });
            }

            var packet = new LeaderBoardResponsePacket
            {
                Type = PacketType.LeaderBoardResponse,
                ClientId = client.Id,
                PacketId = ++client.SendPacketsCounter,
                ClientPacketId = client.ReceivePacketsCounter,
                Players = players.ToArray()
            };

            Server.SendUDPData(client, packet);
            Console.WriteLine("SendLeaderBoardResponse");
        }
    }
}

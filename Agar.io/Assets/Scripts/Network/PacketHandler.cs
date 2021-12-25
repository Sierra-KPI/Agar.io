using UnityEngine;
using Agario.UnityView;
using Agario.Model;

namespace Agario.Network
{
    public class PacketHandler
    {
        public static void SendConnectionRequest(string name)
        {
            var packet = new ConnectionRequestPacket
            {
                Type = PacketType.ConnectionRequest,
                PacketId = ++Client.Instance.SendPacketsCounter,
                Name = name
            };

            Client.Instance.SendUDPData(packet);
        }

        public static void GetConnectionResponse(PacketBase _packet)
        {
            var packet = (ConnectionResponsePacket)_packet;
            Client.Instance.Id = packet.ClientId;
            Client.Instance.ReceivePacketsCounter = packet.PacketId;

            var position = new System.Numerics.Vector2(packet.Position.X, packet.Position.Y);
            Client.Instance.Player.Position = position;

            Timer.Time = packet.GameTime;
            Debug.Log("GetConnectionResponse -> Position: " + Client.Instance.Player.Position.X +
                " " + Client.Instance.Player.Position.Y);
        }

        public static void SendPlayerPosition(float x, float y, float size)
        {
            var packet = new PlayerPosition
            {
                Type = PacketType.PlayerPosition,
                ClientId = Client.Instance.Id,
                PacketId = ++Client.Instance.SendPacketsCounter,
                X = x,
                Y = y,
                Size = size
            };

            Client.Instance.SendUDPData(packet);
        }

        public static void GetBoardUpdate(PacketBase _packet)
        {
            var packet = (BoardUpdatePacket)_packet;

            if (packet.ClientPacketId == Client.Instance.SendPacketsCounter)
            {
                Client.Instance.TimeOfResponse = 0;
            }
            Client.Instance.TimeOfLife = 0;

            Client.Instance.Food = new();
            foreach (var playerPosition in packet.Players)
            {
                if (playerPosition.ClientId == 1000)
                {
                    var food = new Food();
                    food.Position = new System.Numerics.Vector2(playerPosition.X, playerPosition.Y);
                    food.Radius = playerPosition.Size;
                    Client.Instance.Food.Add(food);

                    continue;
                }
                Player player;
                if (Client.Instance.Player.Id == playerPosition.ClientId)
                {
                    player = Client.Instance.Player;
                }
                else if (Client.Instance.Players.ContainsKey(playerPosition.ClientId))
                {
                    player = Client.Instance.Players[playerPosition.ClientId];
                }
                else
                {
                    player = new Player
                    {
                        Id = playerPosition.ClientId
                    };
                    Client.Instance.Players.Add(playerPosition.ClientId, player);
                }

                player.Position = new System.Numerics.Vector2(playerPosition.X, playerPosition.Y);
                player.Radius = playerPosition.Size;

            }

        }

        public static void SendPlayerInfoRequest(int playerId)
        {
            var packet = new PlayerInfoRequestPacket
            {
                Type = PacketType.PlayerInfoRequest,
                ClientId = Client.Instance.Id,
                PacketId = ++Client.Instance.SendPacketsCounter,
                PlayerId = playerId
            };

            Client.Instance.SendUDPData(packet);
            Debug.Log("SendPlayerInfoRequest");
        }

        public static void GetPlayerInfoResponse(PacketBase _packet)
        {
            var packet = (PlayerInfoResponsePacket)_packet;
            Client.Instance.Id = packet.ClientId;
            Client.Instance.ReceivePacketsCounter = packet.PacketId;

            if (packet.PlayerId == 0)
            {
                return;
            }

            var player = new Player
            {
                Name = packet.Player.Name
            };
            Client.Instance.Players[packet.PlayerId] = player;

            Debug.Log("GetPlayerInfoResponse -> Name: " + player.Name);
        }

        public static void SendLeaderBoardRequest()
        {
            var packet = new LeaderBoardRequestPacket
            {
                Type = PacketType.LeaderBoardRequest,
                ClientId = Client.Instance.Id,
                PacketId = ++Client.Instance.SendPacketsCounter,
            };

            Client.Instance.SendUDPData(packet);
            Debug.Log("SendLeaderBoardRequest");
        }

        public static void GetLeaderBoardResponse(PacketBase _packet)
        {
            var packet = (LeaderBoardResponsePacket)_packet;
            Client.Instance.Id = packet.ClientId;
            Client.Instance.ReceivePacketsCounter = packet.PacketId;
            Debug.Log("GetLeaderBoardResponse");

            var leaderBoard = new Player[packet.Players.GetLength(0)];
            for (int i = 0; i < packet.Players.GetLength(0); i++)
            {
                leaderBoard[i] = new Player()
                {
                    Name = packet.Players[i].Name,
                    Radius = packet.Players[i].Size
                };
            }

            EndMenu.Players = leaderBoard;
        }

    }
}

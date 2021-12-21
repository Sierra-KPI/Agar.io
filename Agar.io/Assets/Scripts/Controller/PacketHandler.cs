using UnityEngine;

namespace Agario.Network
{
    internal class PacketHandler
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

            Debug.Log("GetConnectionResponse -> Id: " + packet.ClientId);
        }

        public static void SendPlayerPosition(int x, int y, int size)
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

            // update board from packet

            //Debug.Log("GetBoardUpdate -> PlayersNumber: " + packet.PlayersNumber);
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

            if (packet.PlayerId == 0) return;

            var player = new Player
            {
                Name = packet.PlayerName,
                Color = packet.PlayerColor
            };
            Client.Instance.playersInfo[packet.PlayerId] = player;

            Debug.Log("GetPlayerInfoResponse -> Name: " + packet.PlayerName);
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
        }

    }
}

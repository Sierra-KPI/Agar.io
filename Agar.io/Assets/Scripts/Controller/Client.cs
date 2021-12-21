using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using UnityEngine;

namespace Agario.Network
{

    public struct Player {
        public string Name;
        public string Color;
    }

    public class Client
    {
        public static Client Instance;
        public int Id;
        //public Player player;

        private readonly string _serverIp = "127.0.0.1";
        private readonly int _serverSendPort = 26950;
        private readonly int _serverReceivePort = 26952;
        private IPEndPoint _sendEndPoint;
        private IPEndPoint _receiveEndPoint;
        private readonly UdpClient _udp;

        public int ReceivePacketsCounter = 0;
        public int SendPacketsCounter = 0;
        public int TimeOfLife = 0;
        public int TimeOfResponse = 0;
        public int MaxTimeOfLife = 3; // 1 sec
        public int MaxTimeOfResponse = 40; // 0.1 sec

        private delegate void Handler(PacketBase _packet);
        private static Dictionary<PacketType, Handler> s_packetHandlers;

        public Dictionary<int, Player> playersInfo;

        public Client()
        {
            Instance = this;
            playersInfo = new();

            _udp = new UdpClient();
            InitializeClientData();
            _udp.BeginReceive(UDPReceiveCallback, null);

            PacketHandler.SendConnectionRequest("Player1");
        }

        private void UDPReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint receivePoint = null;
                byte[] data = _udp.EndReceive(result, ref receivePoint);
                _udp.BeginReceive(UDPReceiveCallback, null);

                if (!receivePoint.Equals(_receiveEndPoint))
                {
                    return;
                }

                using (MemoryStream ms = new MemoryStream(data))
                {
                    var packet = Serializer
                        .DeserializeWithLengthPrefix<PacketBase>(ms,
                        PrefixStyle.Base128);
                    s_packetHandlers[packet.Type](packet);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error receiving UDP data: {e}");
            }
        }

        public void SendUDPData(PacketBase packet)
        {
            using (MemoryStream outputFile = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(outputFile, packet,
                    PrefixStyle.Base128);
                var data = outputFile.ToArray();
                _udp.BeginSend(data, data.GetLength(0),
                    _sendEndPoint, null, null);
            }
            //Debug.Log("send data to " + _sendEndPoint.ToString());
        }

        private void InitializeClientData()
        {
            _sendEndPoint = new IPEndPoint(IPAddress.Parse(_serverIp),
                _serverSendPort);
            _receiveEndPoint = new IPEndPoint(IPAddress.Parse(_serverIp),
                _serverReceivePort);

            s_packetHandlers = new Dictionary<PacketType, Handler>()
            {
                { PacketType.ConnectionResponse,
                    PacketHandler.GetConnectionResponse },
                { PacketType.BoardUpdate, PacketHandler.GetBoardUpdate },
                { PacketType.PlayerInfoResponse, PacketHandler.GetPlayerInfoResponse },
            };
        }

        public void CheckConnectToServer()
        {
            if (++TimeOfLife > MaxTimeOfLife)
            {
                Debug.Log("Disconnected from server.");
                PacketHandler.SendConnectionRequest("name"); //fix
            }

            if (TimeOfResponse > MaxTimeOfResponse)
            {
                PacketHandler.SendPlayerPosition(9, 9, 2); // fix
            }
        }
    }
}

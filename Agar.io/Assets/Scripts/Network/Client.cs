using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using UnityEngine;
using Agario.Model;
using Agario.UnityView;

namespace Agario.Network
{
    public class Client
    {
        #region Fields

        public static Client Instance;
        public int Id;
        public Player Player;

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
        public int MaxTimeOfLife = 1;
        public int MaxTimeOfResponse = 40;

        private delegate void Handler(PacketBase _packet);
        private static Dictionary<PacketType, Handler> s_packetHandlers;

        public Dictionary<int, Player> Players;
        public List<Food> Food = new();

        private const string ErrorReceivingDataMessage =
            "Error receiving UDP data: ";
        private const string DisconnectedMessage =
            "Disconnected from server.";

        #endregion Fields

        #region Constructor

        public Client(Player player)
        {
            Instance = this;
            Players = new();
            Player = player;

            _udp = new UdpClient();
            InitializeClientData();
            _udp.BeginReceive(UDPReceiveCallback, null);

            PacketHandler.SendConnectionRequest(Player.Name);
        }

        #endregion Constructor

        #region Methods

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
                Debug.Log(ErrorReceivingDataMessage + e);
            }
        }

        public void SendUDPData(PacketBase packet)
        {
            using (MemoryStream outputFile = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(outputFile, packet,
                    PrefixStyle.Base128);
                byte[] data = outputFile.ToArray();
                _udp.BeginSend(data, data.GetLength(0),
                    _sendEndPoint, null, null);
            }
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
                { PacketType.PlayerInfoResponse,
                    PacketHandler.GetPlayerInfoResponse },
                { PacketType.LeaderBoardResponse,
                    PacketHandler.GetLeaderBoardResponse },
            };
        }

        public void CheckConnectToServer()
        {
            if (++TimeOfLife > MaxTimeOfLife)
            {
                Debug.Log(DisconnectedMessage);
                SceneLoader.ShowDisconnectedMeassage();
                PacketHandler.SendConnectionRequest(Player.Name); 
            }
            else
            {
                SceneLoader.HideDisconnectedMeassage();
            }

            if (TimeOfResponse > MaxTimeOfResponse)
            {
                PacketHandler.SendPlayerPosition(0, 0, Player.Radius); 
            }
        }

        #endregion Methods
    }
}

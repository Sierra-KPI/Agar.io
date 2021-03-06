using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Agario.Model;
using ProtoBuf;

namespace GameServer
{
    internal class Server
    {
        #region Fields

        public static AgarioGame Game;
        private static readonly Dictionary<int, Client> s_clients = new();
        private static int s_clientId = 0;

        private delegate void Handler(Client client, PacketBase packet);
        private static Dictionary<PacketType, Handler> s_packetHandlers;

        private static UdpClient s_udpListener;
        private static UdpClient s_udpSender;

        private const string ServerStartMessage = "Starting server...";
        private const string ServerStartPortMessage = "Server started on port";
        private const string ErrorReceivingDataMessage = 
            "Error receiving UDP data: ";
        private const string InitializedPacketsMessage = "Initialized packets.";

        #endregion Fields

        #region Methods

        public static void Start(int receivePort, int sendPort)
        {
            Game = new AgarioGame();

            Console.WriteLine(ServerStartMessage);
            InitializeServerData();
            
            s_udpListener = new UdpClient(receivePort);
            s_udpListener.BeginReceive(UDPReceiveCallback, null);

            s_udpSender = new UdpClient(sendPort);

            Console.WriteLine(ServerStartPortMessage + receivePort);
        }

        private static void UDPReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = s_udpListener.EndReceive(result,
                    ref clientEndPoint);
                s_udpListener.BeginReceive(UDPReceiveCallback, null);

                using (MemoryStream ms = new MemoryStream(data))
                {
                    var packet =
                        Serializer.DeserializeWithLengthPrefix<PacketBase>
                        (ms, PrefixStyle.Base128);

                    Client client;

                    if (packet.Type == PacketType.ConnectionRequest)
                    {
                        client = AddClient(clientEndPoint);
                    }
                    else
                    {
                        if (!s_clients.ContainsKey(packet.ClientId))
                        {
                            return;
                        }

                        if (s_clients[packet.ClientId].EndPoint ==
                            clientEndPoint)
                        {
                            return;
                        }

                        client = s_clients[packet.ClientId];
                    }

                    s_packetHandlers[packet.Type](client, packet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ErrorReceivingDataMessage + e);
            }
        }

        public static void SendUDPData(Client client, PacketBase packet)
        {
            var clientEndPoint = client.EndPoint;

            try
            {
                if (clientEndPoint != null)
                {
                    using (MemoryStream outputFile = new MemoryStream())
                    {
                        Serializer.SerializeWithLengthPrefix(outputFile,
                            packet, PrefixStyle.Base128);
                        s_udpSender.BeginSend(outputFile.ToArray(),
                            outputFile.ToArray().GetLength(0),
                            clientEndPoint, null, null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending data to " +
                    $"{clientEndPoint} via UDP: {e}");
            }
        }

        private static Client AddClient(IPEndPoint endPoint)
        {
            if (Game.IsEnded)
            {
                Game.Start();
            }
            var player = Game.AddPlayer();
            var client = new Client(++s_clientId, endPoint, player);
            s_clients.Add(client.Id, client);

            return client;
        }

        public static Client GetClient(int id)
        {
            if (!s_clients.ContainsKey(id))
            {
                return new Client();
            }

            return s_clients[id];
        }

        private static void DisconnectClient(Client client)
        {
            s_clients.Remove(client.Id);
            Game.RemovePlayer(client.Player);

            Console.WriteLine($"Disconnect {client.EndPoint} from server");
        }

        public static void Update()
        {
            Game.Update();

            foreach (Client client in s_clients.Values)
            {
                if (++client.TimeOfLife > client.MaxTimeOfLife)
                {
                    DisconnectClient(client);
                    continue;
                }

                PacketHandler.SendBoardUpdate(client);
            }
        }

        private static void InitializeServerData()
        {
            s_packetHandlers = new Dictionary<PacketType, Handler>()
            {
                { PacketType.ConnectionRequest,
                    PacketHandler.GetConnectionRequest },
                { PacketType.PlayerPosition, PacketHandler.GetPlayerPosition },
                { PacketType.PlayerInfoRequest,
                    PacketHandler.GetPlayerInfoRequest },
                { PacketType.LeaderBoardRequest,
                    PacketHandler.GetLeaderBoardRequest },
            };

            Console.WriteLine(InitializedPacketsMessage);
        }

        #endregion Methods
    }
}

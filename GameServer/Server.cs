using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;

namespace GameServer
{
    class Server
    {
        private static Dictionary<int, Client> _clients = new Dictionary<int, Client>();

        private delegate void Handler(Client client, PacketBase packet);
        private static Dictionary<PacketType, Handler> _packetHandlers;

        private static UdpClient _udpListener;
        private static UdpClient _udpSender;

        public static void Start(int receivePort, int sendPort)
        {
            Console.WriteLine("Starting server...");
            InitializeServerData();
            
            _udpListener = new UdpClient(receivePort);
            _udpListener.BeginReceive(UDPReceiveCallback, null);

            _udpSender = new UdpClient(sendPort);

            Console.WriteLine($"Server started on port {receivePort}.");
        }

        private static void UDPReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = _udpListener.EndReceive(result, ref clientEndPoint);
                _udpListener.BeginReceive(UDPReceiveCallback, null);

                using (MemoryStream ms = new MemoryStream(data))
                {
                    var packet = Serializer.DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);

                    Client client;
                    if (packet.Type == PacketType.ConnectionRequest)
                    {
                        client = AddClient(clientEndPoint);
                    }
                    else
                    {
                        if (!_clients.ContainsKey(packet.ClientId)) return;
                        if (_clients[packet.ClientId].EndPoint == clientEndPoint) return;
                        client = _clients[packet.ClientId];
                    }
                    _packetHandlers[packet.Type](client, packet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving UDP data: {e}");
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
                        Serializer.SerializeWithLengthPrefix(outputFile, packet,
                            PrefixStyle.Base128);
                        _udpSender.BeginSend(outputFile.ToArray(), outputFile.ToArray().GetLength(0), clientEndPoint, null, null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending data to {clientEndPoint} via UDP: {e}");
            }
        }

        private static Client AddClient(IPEndPoint endPoint)
        {
            var client = new Client(_clients.Count + 1, endPoint);
            _clients.Add(client.Id, client);
            return client;
        }

        private static void DisconnectClient(Client client)
        {
            _clients.Remove(client.Id);
            Console.WriteLine($"Disconnect {client.EndPoint} from server");
        }

        public static void Update()
        {
            foreach (var client in _clients.Values)
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
            _packetHandlers = new Dictionary<PacketType, Handler>()
            {
                { PacketType.ConnectionRequest, PacketHandler.GetConnectionRequest },
                { PacketType.PlayerPosition, PacketHandler.GetPlayerPosition },
            };

            Console.WriteLine("Initialized packets.");
        }
    }
}

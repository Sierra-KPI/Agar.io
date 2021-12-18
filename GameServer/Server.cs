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
        public static int MaxPlayers = 100;
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public delegate void Handler(Client client, PacketBase packet);
        public static Dictionary<PacketType, Handler> packetHandlers;


        private static UdpClient udpListener;
        private static UdpClient udpSender;


        public static void Start(int port, int port2)
        {
            Port = port;

            Console.WriteLine("Starting server...");
            InitializeServerData();
            
            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            udpSender = new UdpClient(port2);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void UDPReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpListener.EndReceive(result, ref clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

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
                        // check if id exist
                        client = clients[packet.ClientId];
                    }
                    packetHandlers[packet.Type](client, packet);
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
                        udpSender.BeginSend(outputFile.ToArray(), outputFile.ToArray().GetLength(0), clientEndPoint, null, null);
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
            var client = new Client(clients.Count + 1, endPoint);
            clients.Add(client.Id, client);
            return client;
        }

        public static void DisconnectClient(Client client)
        {
            clients.Remove(client.Id);
            Console.WriteLine($"Disconnect {client.EndPoint} from server");
        }

        public static void Update()
        {
            foreach (var client in clients)
            {
                // add const for 10
                if (++client.Value.TimeOfLife > 20)
                {
                    DisconnectClient(client.Value);
                    continue;
                }
                
                PacketHandler.SendBoardUpdate(client.Value);
            }
        }

        private static void InitializeServerData()
        {

            packetHandlers = new Dictionary<PacketType, Handler>()
            {
                { PacketType.ConnectionRequest, PacketHandler.GetConnectionRequest },
                { PacketType.PlayerPosition, PacketHandler.GetPlayerPosition },
            };


            Console.WriteLine("Initialized packets.");
        }
    }
}

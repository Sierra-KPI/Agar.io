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

        public delegate void Handler(PacketBase _packet);
        public static Dictionary<PacketType, Handler> packetHandlers;

        private static UdpClient udpListener;

        public static void Start(int _port)
        {
            Port = _port;

            Console.WriteLine("Starting server...");
            InitializeServerData();
            
            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void UDPReceiveCallback(IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                using (MemoryStream ms = new MemoryStream(_data))
                {
                    var _packet = Serializer.DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);
                    packetHandlers[_packet.Type](_packet);

                    /*int _clientId = _packet.Id;

                    if (_clientId == 0)
                    {
                        return;
                    }

                    if (clients[_clientId].udp.endPoint == null)
                    {
                        clients[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }*/

                    //if (clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        // move to another method handle data
                        //packetHandlers[_packet.Type](_packet);
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving UDP data: {_ex}");
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint, PacketBase _packet)
        {
            try
            {
                if (_clientEndPoint != null)
                {
                    using (MemoryStream outputFile = new MemoryStream())
                    {
                        Serializer.SerializeWithLengthPrefix<PacketBase>(outputFile, _packet,
                            PrefixStyle.Base128);
                        udpListener.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0), _clientEndPoint);
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            }
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<PacketType, Handler>()
            {
                { PacketType.Connection, PacketHandler.Connection },
                { PacketType.PlayerPosition, PacketHandler.PlayerPosition },
            };


            Console.WriteLine("Initialized packets.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
                        /*switch (_packet.Type)
                        {
                            case PacketType.Connection:
                                Console.WriteLine("Name: " + ((ConnectionPacket)_packet).Name);
                                break;
                            case PacketType.PlayerPosition:
                                var packet1 = ((PlayerPosition)_packet);
                                Console.WriteLine("X: " + packet1.X + " Y: " + packet1.Y);
                                break;

                        }*/
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving UDP data: {_ex}");
            }
        }

        /*public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
        {
            try
            {
                if (_clientEndPoint != null)
                {
                    udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            }
        }*/

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

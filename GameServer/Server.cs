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

        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        private static UdpClient udpListener;

        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting server...");
            InitializeServerData();
            Console.WriteLine($"Server started on port {Port}.");

            udpListener = new UdpClient(Port);

            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, Port);
                var data = udpListener.Receive(ref remoteEP); // listen on port 11000
                Console.WriteLine("receive data from " + remoteEP.ToString());
                using (MemoryStream ms = new MemoryStream(data))
                {
                    var packet = Serializer.DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);

                    switch (packet.Type)
                    {
                        case PacketType.Connection:
                            Console.WriteLine("Name: " + ((ConnectionPacket)packet).Name);
                            break;
                        case PacketType.PlayerPosition:
                            var packet1 = ((PlayerPosition)packet);
                            Console.WriteLine("X: " + packet1.X + " Y: " + packet1.Y);
                            break;

                    }


                }
                    


                udpListener.Send(new byte[] { 1 }, 1, remoteEP); // reply back
            }

            //udpListener.BeginReceive(UDPReceiveCallback, null);

            
        }



        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }



            Console.WriteLine("Initialized packets.");
        }
    }
}

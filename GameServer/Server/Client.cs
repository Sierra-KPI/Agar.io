using System.Net;
using Agario.Model;

namespace GameServer
{
    internal class Client
    {
        public int Id;
        public IPEndPoint EndPoint;
        public Player Player;

        public int ReceivePacketsCounter = 0;
        public int SendPacketsCounter = 0;
        public int TimeOfLife = 0;
        public int MaxTimeOfLife = 30 * 3;

        public Client(int clientId, IPEndPoint endPoint, Player player)
        {
            Id = clientId;
            EndPoint = endPoint;
            Player = player;
        }

        public Client() { }
    }
}

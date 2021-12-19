using System.Net;

namespace GameServer
{
    class Client
    {
        public int Id;
        public string Name;
        public IPEndPoint EndPoint;
        //public Player player;
        public int ReceivePacketsCounter = 0;
        public int SendPacketsCounter = 0;
        public int TimeOfLife = 0;
        public int MaxTimeOfLife = 30 * 3; // 3 seconds


        public Client(int clientId, IPEndPoint endPoint)
        {
            Id = clientId;
            EndPoint = endPoint;
            //player = new Player();
        }

        
    }
   
}

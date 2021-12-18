using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public int TimeOfLife;


        public Client(int clientId, IPEndPoint endPoint)
        {
            Id = clientId;
            EndPoint = endPoint;
            //player = new Player();
        }
    }


   
}

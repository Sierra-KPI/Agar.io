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
        public int PacketsCounter = 0;


        public Client(int clientId)
        {
            Id = clientId;
            //player = new Player();
        }
    }


   
}

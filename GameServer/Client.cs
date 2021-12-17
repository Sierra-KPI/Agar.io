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

        public int id;
        //public Player player;
        public UDP udp;

        public Client(int _clientId)
        {
            id = _clientId;
            udp = new UDP(id);
        }
    }


    public class UDP
    {
        public IPEndPoint endPoint;

        private int id;

        public UDP(int _id)
        {
            id = _id;
        }

        public void Connect(IPEndPoint _endPoint)
        {
            endPoint = _endPoint;
        }

        
    }
}

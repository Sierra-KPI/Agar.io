using System;
using System.Threading;

namespace GameServer
{
    internal class Program
    {
        private static bool s_isRunning = false;

        private static void Main(string[] args)
        {
            Console.Title = "Game Server";

            s_isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            int receivePort = 26950;
            int sendPort = 26952;
            Server.Start(receivePort, sendPort);

        }

        public const int TicksPerSec = 30; 
        public const float MsPerTick = 1000f / TicksPerSec;

        // rewrite
        private static void MainThread()
        {
            Console.WriteLine($"Main thread started");
            DateTime _nextLoop = DateTime.Now;

            while (s_isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    Server.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(MsPerTick);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}

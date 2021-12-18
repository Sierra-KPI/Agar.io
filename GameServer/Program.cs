﻿using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            int port = 26950;
            int port2 = 26952;
            Server.Start(port, port2);

        }

        public const int TICKS_PER_SEC = 1; // 10
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC;

        // rewrite
        private static void MainThread()
        {
            Console.WriteLine($"Main thread started");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    Server.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}

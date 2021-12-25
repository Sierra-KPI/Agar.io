using System;
using System.Threading;

namespace GameServer
{
    internal class Program
    {
        private static bool s_isRunning = false;
        private const string MainThreadMessage = "Main thread started";
        private const int ReceivePort = 26950;
        private const int SendPort = 26952;
        public const int TicksPerSec = 30;
        public const float MsPerTick = 1000f / TicksPerSec;

        private static void Main(string[] args)
        {
            Console.Title = "Game Server";

            s_isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(ReceivePort, SendPort);
        }

        // rewrite
        private static void MainThread()
        {
            Console.WriteLine(MainThreadMessage);
            DateTime nextLoop = DateTime.Now;

            while (s_isRunning)
            {
                while (nextLoop < DateTime.Now)
                {
                    Server.Update();

                    nextLoop = nextLoop.AddMilliseconds(MsPerTick);

                    if (nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}

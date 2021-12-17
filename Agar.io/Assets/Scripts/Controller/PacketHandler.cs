using System;

class PacketHandler
{
    public static void Connection(PacketBase _packet)
    {
        //Console.WriteLine("Name: " + ((ConnectionPacket)_packet).Name);
    }

    public static void PlayerPosition(PacketBase _packet)
    {
        var packet1 = ((PlayerPosition)_packet);
        //Console.WriteLine("X: " + packet1.X + " Y: " + packet1.Y);
    }

}


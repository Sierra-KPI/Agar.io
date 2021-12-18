using System;

class PacketHandler
{
    public static void SendConnectionRequest(string name)
    {
        var packet = new ConnectionRequestPacket
        {
            Type = PacketType.ConnectionRequest,
            PacketId = 0,
            Name = name
        };

        Client.SendUDPData(packet);
    }

    public static void GetConnectionResponse(PacketBase _packet)
    {
        var packet = (ConnectionResponsePacket)_packet;
        Client.Instance.Id = packet.ClientId;
    }


    public static void PlayerPosition(PacketBase _packet)
    {
        var packet1 = ((PlayerPosition)_packet);
        //Console.WriteLine("X: " + packet1.X + " Y: " + packet1.Y);
    }

}


using UnityEngine;

class PacketHandler
{
    public static void SendConnectionRequest(string name)
    {
        var packet = new ConnectionRequestPacket
        {
            Type = PacketType.ConnectionRequest,
            PacketId = ++Client.Instance.SendPacketsCounter,
            Name = name
        };

        Client.Instance.SendUDPData(packet);
    }

    public static void GetConnectionResponse(PacketBase _packet)
    {
        var packet = (ConnectionResponsePacket)_packet;
        Client.Instance.Id = packet.ClientId;
        Client.Instance.ReceivePacketsCounter = packet.PacketId;

        Debug.Log("GetConnectionResponse -> Id: " + packet.ClientId);
    }

    public static void SendPlayerPosition(int x, int y)
    {
        var packet = new PlayerPosition
        {
            Type = PacketType.PlayerPosition,
            ClientId = Client.Instance.Id,
            PacketId = ++Client.Instance.SendPacketsCounter,
            X = x,
            Y = y
        };

        Client.Instance.SendUDPData(packet);
    }

    public static void GetBoardUpdate(PacketBase _packet)
    {
        var packet = (BoardUpdatePacket)_packet;

        if (packet.ClientPacketId == Client.Instance.SendPacketsCounter)
        {
            Client.Instance.TimeOfResponse = 0;
        }
        Client.Instance.TimeOfLife = 0;

        // update board from packet

        Debug.Log("GetBoardUpdate -> PlayersNumber: " + packet.PlayersNumber);


    }

}


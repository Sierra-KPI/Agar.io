using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using UnityEngine;

public class Client
{
    public static Client instance;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int id = 0;

    public UdpClient udp;
    public IPEndPoint endPoint;

    public Client()
    {
        instance = this;
        endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        udp = new UdpClient();
        udp.Connect(endPoint);

    }

    public void SendToServerWithAnswer()
    {
        udp.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);
        // then receive data
        var receivedData = udp.Receive(ref endPoint);

        Debug.Log("receive data from " + receivedData[0]);
    }

    public void SendToServer(PacketBase packet)
    {
        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix<PacketBase>(outputFile, packet,
                PrefixStyle.Base128);
            udp.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0));
        }
        Debug.Log("send data to " + endPoint.ToString());
    }

    public void SendConnectionPacket()
    {
        ConnectionPacket packet = new ConnectionPacket
        {
            Type = PacketType.Connection,
            Name = "FirstPlayer"
        };
        SendToServer(packet);
    }

    public void SendPlayerPosition()
    {
        PlayerPosition packet = new PlayerPosition
        {
            Type = PacketType.PlayerPosition,
            X = 3,
            Y = 5
        };

        SendToServer(packet);
    }

    
    
}

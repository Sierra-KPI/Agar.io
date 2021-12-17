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
    public int myId = 0;
    public UDP udp;

    public Client()
    {
        if (instance == null)
        {
            instance = this;
            udp = new UDP();
            udp.Connect();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
        }
    }

    public void SendToServerWithAnswer()
    {
        udp.socket.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);
        // then receive data
        var receivedData = udp.socket.Receive(ref udp.endPoint);

        Debug.Log("receive data from " + receivedData[0]);
    }

    public void SendToServer()
    {
        udp.socket.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);

        Debug.Log("send data to " + udp.endPoint.ToString());
    }

    public void SendConnectionPacket()
    {
        ConnectionPacket packet = new ConnectionPacket
        {
            Type = PacketType.Connection,
            Name = "FirstPlayer"
        };

        

        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix<PacketBase>(outputFile, packet,
                PrefixStyle.Base128);
            udp.socket.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0));
        }
    }

    public void SendPlayerPosition()
    {
        PlayerPosition packet = new PlayerPosition
        {
            Type = PacketType.PlayerPosition,
            Name = "FirstPlayer",
            X = 3,
            Y = 5
        };

        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix(outputFile, packet,
                PrefixStyle.Base128);
            udp.socket.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0));
        }
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }

        public void Connect()
        {
            socket = new UdpClient();

            socket.Connect(endPoint);
            //socket.BeginReceive(ReceiveCallback, null);

            //using (Packet _packet = new Packet())
            //{
                //SendData(_packet);
            //}
        }


    }
    
}

using System;
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

    private string _ip = "127.0.0.1";
    private int _port = 26950;
    private int _id = 0;

    private UdpClient _udp;
    private IPEndPoint _endPoint;

    public delegate void Handler(PacketBase _packet);
    public static Dictionary<PacketType, Handler> packetHandlers;

    public Client()
    {
        instance = this;
        _endPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
        _udp = new UdpClient();
        _udp.Connect(_endPoint);

        _udp.BeginReceive(UDPReceiveCallback, null);

        InitializeClientData();
    }

    private void UDPReceiveCallback(IAsyncResult _result)
    {
        try
        {
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] _data = _udp.EndReceive(_result, ref _clientEndPoint);
            _udp.BeginReceive(UDPReceiveCallback, null);

            using (MemoryStream ms = new MemoryStream(_data))
            {
                var _packet = Serializer.DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);
                packetHandlers[_packet.Type](_packet);

            }
        }
        catch (Exception _ex)
        {
            Debug.LogError($"Error receiving UDP data: {_ex}");
        }
    }

    /*public void SendToServerWithAnswer()
    {
        _udp.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);
        // then receive data
        var receivedData = _udp.Receive(ref _endPoint);

        Debug.Log("receive data from " + receivedData[0]);
    }*/

    public void SendToServer(PacketBase packet)
    {
        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix<PacketBase>(outputFile, packet,
                PrefixStyle.Base128);
            _udp.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0));
        }
        Debug.Log("send data to " + _endPoint.ToString());
    }

    // just for testing
    public void SendConnectionPacket()
    {
        ConnectionPacket packet = new ConnectionPacket
        {
            Type = PacketType.Connection,
            Name = "FirstPlayer"
        };
        SendToServer(packet);
    }

    // just for testing
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

    private static void InitializeClientData()
    {
        packetHandlers = new Dictionary<PacketType, Handler>()
        {
            { PacketType.Connection, PacketHandler.Connection },
            { PacketType.PlayerPosition, PacketHandler.PlayerPosition },
        };

    }

}

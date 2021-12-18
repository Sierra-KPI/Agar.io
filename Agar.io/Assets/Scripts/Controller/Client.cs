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
    public static Client Instance;

    private string _ip = "127.0.0.1";
    private int _port = 26950;
    public int Id;

    // public Player player;

    public int ReceivePacketsCounter = 0;
    public int SendPacketsCounter = 0;
    public int TimeOfLife = 0;
    public int TimeOfResponse = 0;


    private UdpClient _udp;
    private IPEndPoint _endPoint;

    public delegate void Handler(PacketBase _packet);
    public static Dictionary<PacketType, Handler> packetHandlers;

    public Client()
    {
        Instance = this;
        _endPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
        _udp = new UdpClient();
        _udp.Connect(_endPoint);

        _udp.BeginReceive(UDPReceiveCallback, null);

        PacketHandler.SendConnectionRequest("SomeName");
        InitializeClientData();
    }

    private void UDPReceiveCallback(IAsyncResult result)
    {
        try
        {
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = _udp.EndReceive(result, ref _clientEndPoint);
            _udp.BeginReceive(UDPReceiveCallback, null);

            using (MemoryStream ms = new MemoryStream(data))
            {
                var packet = Serializer.DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);
                packetHandlers[packet.Type](packet);

            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error receiving UDP data: {e}");
        }
    }

    public static void SendUDPData(PacketBase packet)
    {
        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix(outputFile, packet,
                PrefixStyle.Base128);
            Instance._udp.Send(outputFile.ToArray(), outputFile.ToArray().GetLength(0));
        }
        Debug.Log("send data to " + Instance._endPoint.ToString());
    }

    private static void InitializeClientData()
    {
        packetHandlers = new Dictionary<PacketType, Handler>()
        {
            { PacketType.ConnectionResponse, PacketHandler.GetConnectionResponse },
            { PacketType.BoardUpdate, PacketHandler.GetBoardUpdate },
        };

    }

}

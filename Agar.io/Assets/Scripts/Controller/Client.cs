using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using UnityEngine;

public class Client
{
    public static Client Instance;
    public int Id;
    // public Player player;

    private string _serverIp = "127.0.0.1";
    private int _serverSendPort = 26950;
    private int _serverReceivePort = 26952;

    public int ReceivePacketsCounter = 0;
    public int SendPacketsCounter = 0;
    public int TimeOfLife = 0;
    public int TimeOfResponse = 0;
    public int MaxTimeOfLife = 1800;
    public int MaxTimeOfResponse = 80;

    private UdpClient _udp;
    private IPEndPoint _sendEndPoint;
    private IPEndPoint _receiveEndPoint;

    private delegate void Handler(PacketBase _packet);
    private static Dictionary<PacketType, Handler> _packetHandlers;

    public Client()
    {
        Instance = this;
        
        _udp = new UdpClient();
        InitializeClientData();
        _udp.BeginReceive(UDPReceiveCallback, null);

        PacketHandler.SendConnectionRequest("SomeName");
    }

    private void UDPReceiveCallback(IAsyncResult result)
    {
        try
        {
            IPEndPoint receivePoint = null;
            byte[] data = _udp.EndReceive(result, ref receivePoint);
            _udp.BeginReceive(UDPReceiveCallback, null);
            if (!receivePoint.Equals(_receiveEndPoint)) return;

            using (MemoryStream ms = new MemoryStream(data))
            {
                var packet = Serializer
                    .DeserializeWithLengthPrefix<PacketBase>(ms, PrefixStyle.Base128);
                _packetHandlers[packet.Type](packet);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error receiving UDP data: {e}");
        }
    }

    public void SendUDPData(PacketBase packet)
    {
        using (MemoryStream outputFile = new MemoryStream())
        {
            Serializer.SerializeWithLengthPrefix(outputFile, packet,
                PrefixStyle.Base128);
            var data = outputFile.ToArray();
            _udp.BeginSend(data, data.GetLength(0),
                _sendEndPoint, null, null);
        }
        Debug.Log("send data to " + _sendEndPoint.ToString());
    }

    private void InitializeClientData()
    {
        _sendEndPoint = new IPEndPoint(IPAddress.Parse(_serverIp), _serverSendPort);
        _receiveEndPoint = new IPEndPoint(IPAddress.Parse(_serverIp), _serverReceivePort);

        _packetHandlers = new Dictionary<PacketType, Handler>()
        {
            { PacketType.ConnectionResponse, PacketHandler.GetConnectionResponse },
            { PacketType.BoardUpdate, PacketHandler.GetBoardUpdate },
        };
    }

    public void CheckConnectToServer()
    {
        if (++TimeOfLife > MaxTimeOfLife)
        {
            Debug.Log("Disconnected from server.");
            PacketHandler.SendConnectionRequest("name"); //fix
        }
        if (++TimeOfResponse > MaxTimeOfResponse)
        {
            PacketHandler.SendPlayerPosition(9, 9); // fix
        }
    }

}

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
    private IPEndPoint _sendPoint;
    private bool isConnected = false;

    public delegate void Handler(PacketBase _packet);
    public static Dictionary<PacketType, Handler> packetHandlers;

    public Client()
    {
        Instance = this;
        _endPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
        _sendPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
        _udp = new UdpClient();
        //_udp.Connect(_endPoint);
        isConnected = true;

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
            // check if receivePoint is server (52)
            //Debug.Log(receivePoint.ToString());
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
            //Disconnect();
        }
    }

    public static void SendUDPData(PacketBase packet)
    {
        try
        {
            using (MemoryStream outputFile = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(outputFile, packet,
                    PrefixStyle.Base128);
                Instance._udp.BeginSend(outputFile.ToArray(),
                    outputFile.ToArray().GetLength(0), Instance._sendPoint, null, null);
            }
            Debug.Log("send data to " + Instance._sendPoint.ToString());
        } catch
        {
            //Instance.Disconnect();
        }
        
    }

    private static void InitializeClientData()
    {
        packetHandlers = new Dictionary<PacketType, Handler>()
        {
            { PacketType.ConnectionResponse, PacketHandler.GetConnectionResponse },
            { PacketType.BoardUpdate, PacketHandler.GetBoardUpdate },
        };

    }

    public void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            _endPoint = null;
            _udp = null;

            Debug.Log("Disconnected from server.");

        }        

    }


    public void CheckConnectToServer()
    {
        // add const
        if (++TimeOfLife > 180)
        {
            Debug.Log("Disconnected from server.");
            PacketHandler.SendConnectionRequest("name"); //fix
        }
        if(++TimeOfResponse > 80)
        {
            PacketHandler.SendPlayerPosition(9, 9); // fix
        }
    }

}

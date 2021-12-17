using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Client client;
    private IPEndPoint ep;

    void Start()
    {
        client = new Client();

       
    }

    private void Update()
    {
        KeyController();
    }

    void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //client.SendToServerWithAnswer();
            client.SendConnectionPacket();
            client.SendPlayerPosition();
        }
    }
    
}

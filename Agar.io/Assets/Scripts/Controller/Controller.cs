using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Client client;

    void Start()
    {
        client = new Client();

       
    }

    private void Update()
    {
        KeyController();
    }

    //just for testing
    void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            client.SendConnectionPacket();
            client.SendPlayerPosition();
        }
    }
    
}

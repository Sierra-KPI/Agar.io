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
        client.TimeOfResponse++;
        
    }

    private void FixedUpdate()
    {
        // set Fixed Timestemps to 1 sec
        client.CheckConnectToServer();
        PacketHandler.SendPlayerPosition(1, 2);
    }

    //just for testing
    void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PacketHandler.SendPlayerPosition(1, 2);
        }
    }
    
}

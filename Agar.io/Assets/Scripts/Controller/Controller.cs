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

    private void FixedUpdate()
    {
        client.CheckConnectToServer();
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

using Agario.Network;
using UnityEngine;

namespace Agario.UnityController
{
    public class Controller : MonoBehaviour
    {
        private Client _client;

        private void Start()
        {
            Time.fixedDeltaTime = 1f;
            _client = new Client();
        }

        private void Update()
        {
            KeyController();
            _client.TimeOfResponse++;
        }

        private void FixedUpdate()
        {
            // set Fixed Timestemps to 1 sec
            _client.CheckConnectToServer();
            PacketHandler.SendPlayerPosition(1, 2, 2);
        }

        //just for testing
        private void KeyController()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PacketHandler.SendPlayerPosition(1, 2, 2);
            }
        }
    }
}

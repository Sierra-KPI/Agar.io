using Agario.Model;
using Agario.Network;
using Agario.UnityView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agario.UnityController
{
    public class Controller : MonoBehaviour
    {
        private Client _client;
        private View _view;
        private Timer _timer;
        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField]
        private GameObject _foodPrefab;
        private Entity _player;

        private readonly string _endSceneName = "EndScene";

        private void Start()
        {
            Time.fixedDeltaTime = 1f;
            _client = new Client();

            _timer = gameObject.AddComponent<Timer>();
            _view = gameObject.AddComponent<View>();
            //_view.CreatePlayer(_player);
            _view.CreateEntityObjects(_foodPrefab);
        }

        private void Update()
        {
            ReadMove();
            
            _client.TimeOfResponse++;
        }

        private void FixedUpdate()
        {
            UpdateTimer();
            _client.CheckConnectToServer();
            PacketHandler.SendPlayerPosition(1, 2, 2);
        }

        private void ReadMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            PacketHandler.SendPlayerPosition(horizontal, vertical, 2);
        }

        private void UpdateTimer()
        {
            _timer.UpdateTimer();
            if (Timer.Time >= Timer.MaxTime)
            {
                PacketHandler.SendLeaderBoardRequest();
                SceneManager.LoadScene(_endSceneName);
            }
        }
    }
}

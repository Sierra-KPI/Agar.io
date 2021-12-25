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
        private Player _player;
        private View _view;
        private Timer _timer;

        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField]
        private GameObject _foodPrefab;
        //private Entity _player;

        private readonly string _endSceneName = "EndScene";

        private void Start()
        {
            Time.fixedDeltaTime = 1f;
            _player = new Player()
            {
                Name = View.s_username,
            };
            _client = new Client(_player);

            _timer = gameObject.AddComponent<Timer>();
            _view = gameObject.AddComponent<View>();
            _view.CreateEntityObjects(_foodPrefab, _playerPrefab);
            _view.CreatePlayer(_player);
        }

        private void Update()
        {
            ReadMove();
            _client.TimeOfResponse++;
            UpdateEntitiesPosition();
        }

        private void FixedUpdate()
        {
            UpdateTimer();
            _client.CheckConnectToServer();
            PacketHandler.SendPlayerPosition(0, 0, _player.Radius);
        }

        private void ReadMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            

            PacketHandler.SendPlayerPosition(horizontal, vertical, _player.Radius);
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

        private void UpdateEntitiesPosition()
        {
            _view.ChangePlayersPosition(Client.Instance.Players);
            _view.ChangeFoodPosition(Client.Instance.Food);
        }
    }
}

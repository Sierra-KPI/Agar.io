using Agario.Model;
using Agario.Network;
using Agario.UnityView;
using UnityEngine;

namespace Agario.UnityController
{
    public class Controller : MonoBehaviour
    {
        #region Fields

        private Client _client;
        private Player _player;
        private View _view;
        private Timer _timer;

        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField]
        private GameObject _foodPrefab;

        #endregion Fields

        #region Methods

        private void Start()
        {
            Time.fixedDeltaTime = 1f;
            SceneLoader.SetDeadMenu();

            _player = new Player()
            {
                Name = View.s_username,
            };

            _client = new Client(_player);

            _timer = gameObject.AddComponent<Timer>();
            _view = gameObject.AddComponent<View>();
            _view.CreateEntityObjects(_foodPrefab, _playerPrefab);
        }

        private void Update()
        {
            if (Client.Instance.Player.Radius == 0)
            {
                SceneLoader.LoadDeadMenu();
            }
            else
            {
                ReadMove();
                _client.TimeOfResponse++;
                UpdateEntitiesPosition();
            }
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

            var position = new Vector3(_player.Position.X, _player.Position.Y);
            GetComponentInChildren<Camera>().transform.position = position;

            PacketHandler.SendPlayerPosition(horizontal, vertical, 
                _player.Radius);
        }

        private void UpdateTimer()
        {
            _timer.UpdateTimer();

            if (Timer.Time >= Timer.MaxTime)
            {
                PacketHandler.SendLeaderBoardRequest();
                SceneLoader.LoadEndScene();
            }
        }

        private void UpdateEntitiesPosition()
        {
            _view.ChangePlayersPosition(Client.Instance.Players);
            _view.ChangeFoodPosition(Client.Instance.Food);
        }

        #endregion Methods
    }
}

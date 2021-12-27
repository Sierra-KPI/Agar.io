using Agario.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class EndMenu : MonoBehaviour
    {
        #region Fields

        public static Player[] Players;
        [SerializeField]
        private GameObject _rowPrefab;

        private const int MaxLeaderBoardPlayers = 5;
        private const string TableName = "Table";
        private const string StartButtonName = "StartButton";
        private const string EndButtonName = "EndButton";

        #endregion Fields

        #region Methods

        void Start()
        {
            var table = GameObject.Find(TableName);
            var count = Players.GetLength(0) < MaxLeaderBoardPlayers ?
                Players.GetLength(0) : MaxLeaderBoardPlayers;

            for (var i = 0; i < count; i++)
            {
                GameObject row = Instantiate(_rowPrefab, table.transform);
                Text[] cells = row.GetComponentsInChildren<Text>();
                cells[0].text = (i + 1).ToString();
                cells[1].text = Players[i].Name;
                cells[2].text = Players[i].Radius.ToString("f0");
            }

            var _startButton = GameObject.Find(StartButtonName).
                GetComponent<Button>();
            _startButton.onClick.AddListener(
                delegate { SceneLoader.LoadMainScene(); });

            var _quitButton = GameObject.Find(EndButtonName).
                GetComponent<Button>();
            _quitButton.onClick.AddListener(
                delegate { SceneLoader.QuitButton(); });
        }

        #endregion Methods
    }
}

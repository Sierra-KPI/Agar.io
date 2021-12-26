using Agario.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class EndMenu : MonoBehaviour
    {
        public static Player[] Players;
        [SerializeField]
        private GameObject _rowPrefab;

        void Start()
        {
            var table = GameObject.Find("Table");
            var count = Players.GetLength(0) < 5 ? Players.GetLength(0) : 5;
            for (int i = 0; i < count; i++)
            {
                GameObject row = Instantiate(_rowPrefab, table.transform);
                Text[] cells = row.GetComponentsInChildren<Text>();
                cells[0].text = (i + 1).ToString();
                cells[1].text = Players[i].Name;
                cells[2].text = Players[i].Radius.ToString("f0");
            }

            var _startButton = GameObject.Find("StartButton").GetComponent<Button>();
            _startButton.onClick.AddListener(delegate { SceneLoader.LoadMainScene(); });

            var _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
            _quitButton.onClick.AddListener(delegate { SceneLoader.QuitButton(); });
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class SceneLoader : MonoBehaviour
    {
        private static readonly string _mainSceneName = "MainScene";
        private static readonly string _endSceneName = "EndScene";
        private static readonly string _disconnectedMessage = "Disconnected from server...";

        private static GameObject _deadMenu;

        public static void ShowDisconnectedMeassage()
        {
            var message = GameObject.Find("DisconnectMessage").GetComponent<Text>();
            message.text = _disconnectedMessage;
        }

        public static void HideDisconnectedMeassage()
        {
            var message = GameObject.Find("DisconnectMessage").GetComponent<Text>();
            message.text = "";
        }

        public static void SetDeadMenu()
        {
            _deadMenu = GameObject.Find("DeadMenu");

            var _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
            _quitButton.onClick.AddListener(delegate { QuitButton(); });

            var _startButton = GameObject.Find("StartButton").GetComponent<Button>();
            _startButton.onClick.AddListener(delegate { LoadMainScene(); });

            _deadMenu.SetActive(false);
        }

        public static void LoadDeadMenu()
        {
            _deadMenu.SetActive(true);
        }

        public static void LoadMainScene()
        {
            SceneManager.LoadScene(_mainSceneName);
        }

        public static void LoadEndScene()
        {
            SceneManager.LoadScene(_endSceneName);
        }

        public static void QuitButton()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }

    }
}

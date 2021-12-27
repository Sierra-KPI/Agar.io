using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class SceneLoader : MonoBehaviour
    {
        #region Fields

        private static readonly string _mainSceneName = "MainScene";
        private static readonly string _endSceneName = "EndScene";
        private static readonly string _disconnectMessageName =
            "DisconnectMessage";
        private static readonly string _deadMenuName =
            "DeadMenu";
        private static readonly string _quitButtonName =
            "QuitButton";
        private static readonly string _startButtonName =
            "StartButton";

        private static readonly string _disconnectedMessage =
            "Disconnected from server...";
        private static readonly string _quitMessage = "QUIT";

        private static GameObject _deadMenu;

        #endregion Fields

        #region Methods

        public static void ShowDisconnectedMeassage()
        {
            var message = GameObject.Find(_disconnectMessageName).
                GetComponent<Text>();
            message.text = _disconnectedMessage;
        }

        public static void HideDisconnectedMeassage()
        {
            var message = GameObject.Find(_disconnectMessageName).
                GetComponent<Text>();
            message.text = "";
        }

        public static void SetDeadMenu()
        {
            _deadMenu = GameObject.Find(_deadMenuName);

            var _quitButton = GameObject.Find(_quitButtonName).
                GetComponent<Button>();
            _quitButton.onClick.AddListener(delegate { QuitButton(); });

            var _startButton = GameObject.Find(_startButtonName).
                GetComponent<Button>();
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
            Debug.Log(_quitMessage);
            Application.Quit();
        }

        #endregion Methods
    }
}

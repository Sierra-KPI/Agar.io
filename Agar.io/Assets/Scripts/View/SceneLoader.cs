using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class SceneLoader : MonoBehaviour
    {
        #region Fields

        private static readonly string s_mainSceneName = "MainScene";
        private static readonly string s_endSceneName = "EndScene";
        private static readonly string s_disconnectMessageName =
            "DisconnectMessage";
        private static readonly string s_deadMenuName =
            "DeadMenu";
        private static readonly string s_quitButtonName =
            "QuitButton";
        private static readonly string s_startButtonName =
            "StartButton";

        private static readonly string s_disconnectedMessage =
            "Disconnected from server...";
        private static readonly string s_quitMessage = "QUIT";

        private static GameObject s_deadMenu;

        #endregion Fields

        #region Methods

        public static void ShowDisconnectedMeassage()
        {
            var message = GameObject.Find(s_disconnectMessageName).
                GetComponent<Text>();
            message.text = s_disconnectedMessage;
        }

        public static void HideDisconnectedMeassage()
        {
            var message = GameObject.Find(s_disconnectMessageName).
                GetComponent<Text>();
            message.text = "";
        }

        public static void SetDeadMenu()
        {
            s_deadMenu = GameObject.Find(s_deadMenuName);

            var _quitButton = GameObject.Find(s_quitButtonName).
                GetComponent<Button>();
            _quitButton.onClick.AddListener(delegate { QuitButton(); });

            var _startButton = GameObject.Find(s_startButtonName).
                GetComponent<Button>();
            _startButton.onClick.AddListener(delegate { LoadMainScene(); });

            s_deadMenu.SetActive(false);
        }

        public static void LoadDeadMenu()
        {
            s_deadMenu.SetActive(true);
        }

        public static void LoadMainScene()
        {
            SceneManager.LoadScene(s_mainSceneName);
        }

        public static void LoadEndScene()
        {
            SceneManager.LoadScene(s_endSceneName);
        }

        public static void QuitButton()
        {
            Debug.Log(s_quitMessage);
            Application.Quit();
        }

        #endregion Methods
    }
}

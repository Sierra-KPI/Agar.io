using System.Collections;
using System.Collections.Generic;
using Agario.UnityView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    #region Fields

    private Button _connectButton;
    private Button _quitButton;
    private InputField _inputField;
    private readonly int _usernameLengthLimit = 12;

    private static readonly string s_connectButtonName =
        "ConnectButton";
    private static readonly string s_quitButtonName =
        "QuitButton";
    private static readonly string s_inputFieldName =
        "InputField";
    private static readonly string s_connectMessage =
        "Connect: ";

    #endregion Fields

    #region Methods

    void Start()
    {
        _connectButton = GameObject.Find(s_connectButtonName).
            GetComponent<Button>();
        _connectButton.onClick.AddListener(
            delegate { OnConnectButtonClick(); });

        _quitButton = GameObject.Find(s_quitButtonName).
            GetComponent<Button>();
        _quitButton.onClick.AddListener(
            delegate { SceneLoader.QuitButton(); });

        _inputField = GameObject.Find(s_inputFieldName).
            GetComponent<InputField>();
        _inputField.characterLimit = _usernameLengthLimit;
    }

    private void OnConnectButtonClick()
    {
        var username = _inputField.text;
        Debug.Log(s_connectMessage + username);

        View.s_username = username;

        SceneLoader.LoadMainScene();
    }

    #endregion Methods
}

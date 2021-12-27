using System.Collections;
using System.Collections.Generic;
using Agario.UnityView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private Button _connectButton;
    private Button _quitButton;
    private InputField _inputField;
    private readonly int _usernameLengthLimit = 12;

    void Start()
    {
        _connectButton = GameObject.Find("ConnectButton").GetComponent<Button>();
        _connectButton.onClick.AddListener(delegate { OnConnectButtonClick(); });

        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { SceneLoader.QuitButton(); });

        _inputField = GameObject.Find("InputField").GetComponent<InputField>();
        _inputField.characterLimit = _usernameLengthLimit;
    }

    private void OnConnectButtonClick()
    {
        var username = _inputField.text;
        Debug.Log("Connect: " + username);

        View.s_username = username;

        SceneLoader.LoadMainScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private string _mainSceneName = "MainScene";
    private Button _connectButton;
    private Button _quitButton;
    private InputField _inputField;
    private int _usernameLengthLimit = 12;

    void Start()
    {
        _connectButton = GameObject.Find("ConnectButton").GetComponent<Button>();
        _connectButton.onClick.AddListener(delegate { OnConnectButtonClick(); });

        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { OnQuitButtonClick(); });

        _inputField = GameObject.Find("InputField").GetComponent<InputField>();
        _inputField.characterLimit = _usernameLengthLimit;
    }

    private void OnConnectButtonClick()
    {
        var username = _inputField.text;
        Debug.Log("Connect: " + username);
        // add static field to view for username
        SceneManager.LoadScene(_mainSceneName);
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

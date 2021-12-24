using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    private readonly string _mainSceneName = "MainScene";
    private Button _quitButton;
    private Button _startButton;


    void Start()
    {

        _startButton = GameObject.Find("StartButton").GetComponent<Button>();
        _startButton.onClick.AddListener(delegate { OnStartButtonClick(); });

        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { OnQuitButtonClick(); });

    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(_mainSceneName);
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using Agario.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public static Player[] Players;
    private readonly string _mainSceneName = "MainScene";
    private Button _quitButton;
    private Button _startButton;
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

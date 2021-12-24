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
    [SerializeField]
    private GameObject _rowPrefab;

    void Start()
    {
        var table = GameObject.Find("Table");

        for (int i = 0; i < 5; i++)
        {
            GameObject row = Instantiate(_rowPrefab, table.transform);
            Text[] cells = row.GetComponentsInChildren<Text>();
            cells[0].text = (i + 1).ToString();
            cells[1].text = "Username" + i;
            cells[2].text = ((5-i)*6.829).ToString("f0");
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

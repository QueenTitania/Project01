using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pausePanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void QuitLevel()
    {
        Application.Quit();
    }

    public void WinMenu()
    {
        winPanel.SetActive(true);
    }

    public void LoseMenu()
    {
        losePanel.SetActive(true);
    }

}

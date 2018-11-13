using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float m_delayInSeconds = 3.5f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");

        try
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
        catch (NullReferenceException)
        {            
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(m_delayInSeconds);

        SceneManager.LoadScene("GameOver");
    }
}

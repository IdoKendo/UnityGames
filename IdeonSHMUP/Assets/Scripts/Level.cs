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
        FindObjectOfType<GameSession>().StartMenu();
    }

    public void TryAgain()
    {
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().NextCutscene();
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

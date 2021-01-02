using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private AudioClip m_thousandPointsSfx;
    [SerializeField] private float m_thousandPointsVolume = 0.25f;
    [SerializeField] private int m_healthIncrease = 100;
    [SerializeField] private int m_finalLevel = 2;

    private int m_score = 0;
    private int m_sceneCounter = 0;

    public int Score { get { return m_score; } }

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int i_scoreValue)
    {
        m_score += i_scoreValue;

        if (m_score % 1000 == 0)
        {
            AudioSource.PlayClipAtPoint(m_thousandPointsSfx, Camera.main.transform.position, m_thousandPointsVolume);

            FindObjectOfType<Player>().Health += m_healthIncrease;
        }
    }

    public void NextLevel()
    {
        string nextScene = string.Concat("Level-", m_sceneCounter.ToString());

        SceneManager.LoadScene(nextScene);
    }

    public void NextCutscene()
    {
        if (m_sceneCounter >= m_finalLevel)
        {
            SceneManager.LoadScene("GameOver"); // Temporary, until victory screen implementation
        }

        string nextScene = string.Concat("Cutscene-", m_sceneCounter.ToString());

        m_sceneCounter++;
        GetBonusForRemainingLife();
        SceneManager.LoadScene(nextScene);
    }

    public void ResetGame()
    {
        m_score = 0;
        m_sceneCounter = 1;

        NextLevel();
    }

    public void StartMenu()
    {
        m_score = 0;
        m_sceneCounter = 0;

        SceneManager.LoadScene("StartMenu");
    }

    private void GetBonusForRemainingLife()
    {
        try
        {
            m_score += FindObjectOfType<Player>().Health * 10;

        }
        catch (NullReferenceException)
        {
        }
    }
}

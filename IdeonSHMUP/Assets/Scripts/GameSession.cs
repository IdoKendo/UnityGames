using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int m_score = 0;

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
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private AudioClip m_thousandPointsSfx;
    [SerializeField] private float m_thousandPointsVolume = 0.25f;

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

        if (m_score % 1000 == 0)
        {
            AudioSource.PlayClipAtPoint(m_thousandPointsSfx, Camera.main.transform.position, m_thousandPointsVolume);
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text m_scoreText;
    private GameSession m_gameSession;

    void Start()
    {
        m_scoreText = GetComponent<Text>();
        m_gameSession = FindObjectOfType<GameSession>();
    }
    
    private void Update()
    {
        m_scoreText.text = m_gameSession.Score.ToString();
    }
}

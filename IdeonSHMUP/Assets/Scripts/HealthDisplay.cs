using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Text m_healthText;
    private Player m_player;

    void Start()
    {
        m_healthText = GetComponent<Text>();
        m_player = FindObjectOfType<Player>();
    }
    
    private void Update()
    {
        m_healthText.text = m_player.Health.ToString();
    }
}

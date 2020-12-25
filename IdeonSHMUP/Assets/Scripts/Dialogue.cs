using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI m_textDisplay;
    [SerializeField] private float m_typingSpeed = 0.02f;
    [SerializeField] private float m_lingerDuration = 3f;
    [SerializeField] private string[] m_sentences;
    [SerializeField] private AudioClip m_typingSound;
    [SerializeField] private float m_typingVolume = 0.1f;

    private int m_index;
    private Coroutine m_typingCoroutine;

    private IEnumerator Type()
    {
        foreach (char letter in m_sentences[m_index].ToCharArray())
        {
            m_textDisplay.text += letter;
            if (letter != ' ')
            {
                AudioSource.PlayClipAtPoint(m_typingSound, Camera.main.transform.position, m_typingVolume);
            }

            yield return new WaitForSeconds(m_typingSpeed);
        }
    }

    private IEnumerator WaitOnLastSentence()
    {
        yield return new WaitForSeconds(m_lingerDuration);

        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        m_typingCoroutine = StartCoroutine(Type());
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            NextSentence();
        }

        if(m_index == m_sentences.Length - 1)
        {
            StartCoroutine(WaitOnLastSentence());
        }
    }

    private void NextSentence()
    {
        StopCoroutine(m_typingCoroutine);
        if (m_index < m_sentences.Length - 1)
        {
            if (m_textDisplay.text.Length < m_sentences[m_index].ToCharArray().Length)
            {
                m_textDisplay.text = m_sentences[m_index];
            }
            else
            {
                m_index++;
                m_textDisplay.text = "";
                m_typingCoroutine = StartCoroutine(Type());
            }
        } else
        {
            m_textDisplay.text = m_sentences[m_sentences.Length - 1];
        }
    }
}

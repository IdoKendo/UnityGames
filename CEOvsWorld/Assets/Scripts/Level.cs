using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float m_timeToWait = 3.5f;

    private int currentSceneIdx;

    private void Start()
    {
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIdx == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(m_timeToWait);

        LoadNextScene();

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIdx + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

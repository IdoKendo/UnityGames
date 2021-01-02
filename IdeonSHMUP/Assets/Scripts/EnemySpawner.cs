using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> m_waveConfigurations;
    [SerializeField] private int m_startingWave = 0;
    [SerializeField] private float m_startDelay = 1f;
    [SerializeField] private int m_loopCount = 2;

    private bool m_spawnedAllWaves = false;

    private void Start()
    {
        Invoke("Begin", m_startDelay);
    }

    private void Update()
    {
        if (m_spawnedAllWaves)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length == 0)
            {
                /* If finished spawning all waves and the last enemy spawned is dead
                Then GameSession will move us to the next level! */
                FindObjectOfType<GameSession>().NextCutscene();
            }
        }
    }

    private void Begin()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        for (int i = 0; i < m_loopCount; i++)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }

        m_spawnedAllWaves = true;

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = m_startingWave; i < m_waveConfigurations.Count; i++)
        {
            WaveConfig currentWave = m_waveConfigurations[i];
            
            yield return StartCoroutine(SpawnWave(currentWave));
        }
    }

    private IEnumerator SpawnWave(WaveConfig i_waveConfig)
    {
        for (int i = 0; i < i_waveConfig.EnemyCount; i++)
        {
            GameObject newEnemy = Instantiate(i_waveConfig.EnemyPrefab,
                i_waveConfig.Waypoints[0].transform.position, Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().WaveConfiguration = i_waveConfig;

            yield return new WaitForSeconds(i_waveConfig.SpawnDelay);
        }
    }
}

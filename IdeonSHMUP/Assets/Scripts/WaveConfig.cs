using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private GameObject m_pathPrefab;
    [SerializeField] private float m_spawnDelay = 0.5f;
    [SerializeField] private float m_spawnRngFactor = 0.3f;
    [SerializeField] private int m_enemyCount = 5;
    [SerializeField] private float m_moveSpeed = 4f;

    public GameObject EnemyPrefab { get { return m_enemyPrefab; } }

    public List<Transform> Waypoints
    {
        get
        {
            List<Transform> waveWaypoints = new List<Transform>();

            foreach (Transform waypoint in m_pathPrefab.transform)
            {
                waveWaypoints.Add(waypoint);
            }

            return waveWaypoints;
        }
    }

    public float SpawnDelay { get { return m_spawnDelay; } }

    public float SpawnRngFactor { get { return m_spawnRngFactor; } }

    public int EnemyCount { get { return m_enemyCount; } }

    public float MoveSpeed { get { return m_moveSpeed; } }
}

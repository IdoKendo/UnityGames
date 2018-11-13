using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private List<Transform> m_waypoints;
    private int waypointIndex = 0;

    public WaveConfig WaveConfiguration { get; set; }

    private void Start()
    {
        m_waypoints = WaveConfiguration.Waypoints;
        transform.position = m_waypoints[waypointIndex].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= m_waypoints.Count - 1)
        {
            Vector3 target = m_waypoints[waypointIndex].transform.position;
            float movementThisFrame = WaveConfiguration.MoveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position, target, movementThisFrame);

            if (transform.position == target)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

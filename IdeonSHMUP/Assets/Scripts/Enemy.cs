using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float m_health = 100;
    [SerializeField] private int m_scoreValue = 100;
    [SerializeField] private float m_padding = 0.65f;

    [Header("Projectile")]
    [SerializeField] private float m_minTimeBetweenShots = 0.2f;
    [SerializeField] private float m_maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_projectileSpeed = 10f;
    [SerializeField] private AudioClip m_projectileSfx;
    [SerializeField] [Range(0, 1)] private float m_projectileVolume = 0.25f;

    [Header("Death")]
    [SerializeField] private GameObject m_explosionParticles;
    [SerializeField] private float m_explosionDuration = 1f;
    [SerializeField] private AudioClip m_explosionSfx;
    [SerializeField] [Range(0, 1)] private float m_explosionVolume = 0.75f;

    private float m_shotCounter;

    private void Start()
    {
        ResetShotCounter();
    }
    
    private void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        m_shotCounter -= Time.deltaTime;

        if (m_shotCounter <= 0)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void ResetShotCounter()
    {
        m_shotCounter = Random.Range(m_minTimeBetweenShots, m_maxTimeBetweenShots);
    }

    private void Fire()
    {
        Vector3 projectilePosition = new Vector3()
        {
            x = transform.position.x,
            y = transform.position.y - m_padding,
            z = transform.position.z
        };

        GameObject projectile = Instantiate(m_projectilePrefab,
            projectilePosition, Quaternion.identity) as GameObject;

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_projectileSpeed);
        AudioSource.PlayClipAtPoint(m_projectileSfx, Camera.main.transform.position, m_projectileVolume);
    }

    private void OnTriggerEnter2D(Collider2D i_other)
    {
        DamageDealer damageDealer = i_other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer i_damageDealer)
    {
        m_health -= i_damageDealer.Damage;
        i_damageDealer.Hit();

        if (m_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosion = Instantiate(m_explosionParticles, transform.position, Quaternion.identity);

        FindObjectOfType<GameSession>().AddToScore(m_scoreValue);
        Destroy(gameObject);
        Destroy(explosion, m_explosionDuration);
        AudioSource.PlayClipAtPoint(m_explosionSfx, Camera.main.transform.position, m_explosionVolume);
    }
}

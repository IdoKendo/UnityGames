﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_moveSpeed = 15f;
    [SerializeField] private float m_padding = 0.65f;
    [SerializeField] private int m_health = 200;
    [SerializeField] private AudioClip m_explosionSfx;
    [SerializeField] [Range(0, 1)] private float m_explosionVolume = 0.75f;

    [Header("Projectile")]
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_projectileSpeed = 20f;
    [SerializeField] private float m_projectileFiringPeriod = 0.15f;
    [SerializeField] private AudioClip m_projectileSfx;
    [SerializeField] [Range(0, 1)] private float m_projectileVolume = 0.1f;

    [Header("Jank Controls Settings")]
    [SerializeField] private bool m_jankControlScheme = true;
    [SerializeField] private float m_nextJankFire = 0.0f;
    [SerializeField] private float m_delayBetweenShots = 0.2f;

    private Coroutine m_firingCoroutine;
    private float m_minX;
    private float m_maxX;
    private float m_minY;
    private float m_maxY;
    private float m_actualSpeed;

    public int Health { get { return m_health; } }

    private void Start()
    {
        BoundariesSetup();
        m_actualSpeed = m_moveSpeed;
    }

    private void Update()
    {
        DetermineSpeed();
        Move();
        Fire();
    }

    private void DetermineSpeed()
    {
        if (!m_jankControlScheme)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                m_actualSpeed = m_moveSpeed * 0.5f;
            }
            else if (Input.GetButtonUp("Fire3"))
            {
                m_actualSpeed = m_moveSpeed;
            }
        }
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
            m_health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(m_explosionSfx, Camera.main.transform.position, m_explosionVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void Move()
    {
        if (m_jankControlScheme)
        {
            var target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            var start = transform.position;
            var diff = target - start;
            transform.position = new Vector2()
            {
                x = Mathf.Clamp(transform.position.x + diff.x, m_minX, m_maxX),
                y = Mathf.Clamp(transform.position.y + diff.y, m_minY, m_maxY)
            };
        }
        else
        {
            float deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * m_actualSpeed;
            float deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * m_actualSpeed;

            transform.position = new Vector2()
            {
                x = Mathf.Clamp(transform.position.x + deltaX, m_minX, m_maxX),
                y = Mathf.Clamp(transform.position.y + deltaY, m_minY, m_maxY)
            };
        }
    }

    private void Fire()
    {
        if (m_jankControlScheme)
        {
            if (Time.time > m_nextJankFire)
            {
                m_firingCoroutine = StartCoroutine(ContinuousFire());
                m_nextJankFire += m_delayBetweenShots;
                StopCoroutine(m_firingCoroutine);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_firingCoroutine = StartCoroutine(ContinuousFire());
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(m_firingCoroutine);
            }
        }
    }

    IEnumerator ContinuousFire()
    {
        while (true)
        {
            Vector3 projectilePosition = new Vector3()
            {
                x = transform.position.x,
                y = transform.position.y,
                z = transform.position.z
            };

            GameObject projectile = Instantiate(m_projectilePrefab,
                projectilePosition, Quaternion.identity) as GameObject;

            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_projectileSpeed);
            AudioSource.PlayClipAtPoint(m_projectileSfx, Camera.main.transform.position, m_projectileVolume);

            yield return new WaitForSeconds(m_projectileFiringPeriod);
        }
    }

    private void BoundariesSetup()
    {
        Camera gameCamera = Camera.main;
        Vector3 minVector = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxVector = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        m_minX = minVector.x + m_padding;
        m_maxX = maxVector.x - m_padding;
        m_minY = minVector.y + m_padding;
        m_maxY = maxVector.y - m_padding;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int m_damage = 100;
    [SerializeField] private AudioClip m_damageSfx;
    [SerializeField] private float m_damageVolume = 0.9f;

    public int Damage { get { return m_damage; } }

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(m_damageSfx, Camera.main.transform.position, m_damageVolume);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int m_damage = 100;

    public int Damage { get { return m_damage; } }

    public void Hit()
    {
        Destroy(gameObject);
    }
}

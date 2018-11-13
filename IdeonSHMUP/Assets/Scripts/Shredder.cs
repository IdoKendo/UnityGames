using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D i_collision)
    {
        Destroy(i_collision.gameObject);
    }
}

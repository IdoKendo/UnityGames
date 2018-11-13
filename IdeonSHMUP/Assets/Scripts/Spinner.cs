using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float m_SpinSpeed = 720f;

    private void Update()
    {
        transform.Rotate(0, 0, m_SpinSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] private float m_scrollSpeed = 0.2f;

    private Material m_material;
    private Vector2 m_offset;
    
    void Start()
    {
        m_material = GetComponent<Renderer>().material;
        m_offset = new Vector2(0f, m_scrollSpeed);
    }
    
    void Update()
    {
        m_material.mainTextureOffset += m_offset * Time.deltaTime;
    }
}

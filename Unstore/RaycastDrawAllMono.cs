using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDrawAllMono : MonoBehaviour
{
    public Transform m_direction;
    public LayerMask m_layers;
    public float m_touchLength = 0.02f;
    public Color m_color = Color.cyan;
    void Update()
    {
        RaycastHit[] hit = Physics.RaycastAll(m_direction.position, m_direction.forward, float.MaxValue, m_layers);
        if (hit.Length == 0)
        {
            Debug.DrawLine(m_direction.position, m_direction.position + m_direction.forward * 100, Color.red);
        }

        else {
            for (int i = 0; i < hit.Length; i++)
            {

                Debug.DrawLine(hit[i].point-m_direction.forward*0.02f, hit[i].point + m_direction.forward * 0.02f  , m_color);
            }
            
        }

    }
    
    private void Reset()
    {
        m_direction = transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDrawSoloMono : MonoBehaviour
{
    public Transform m_direction;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_direction.position, m_direction.forward, out hit))

            Debug.DrawLine(m_direction.position, hit.point, Color.green);
        else
            Debug.DrawLine(m_direction.position, m_direction.position + m_direction.forward * 100, Color.red);
    }
}

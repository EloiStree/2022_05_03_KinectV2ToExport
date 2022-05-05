using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_TransformToPercentMono : MonoBehaviour
{
    public WorldPositionToPlayZoneSquarePourcentMono m_worldToPercent;
    public Transform m_pointTracked;
    public bool m_inverse;
    [Range(0f,1f)]
    public float m_horizontalPercent;
    [Range(0f, 1f)]
    public float m_verticalPercent;
   

    void Update()
    {
        m_worldToPercent.GetPercent(m_pointTracked, m_inverse,out  m_horizontalPercent,out  m_verticalPercent);
    }
}

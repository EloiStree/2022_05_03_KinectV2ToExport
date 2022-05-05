using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_PlayZoneToPercentMono : MonoBehaviour
{

    public PlayZoneWorldSpaceDebugMono m_localPlayZone;
    public Transform m_target;

    public bool m_useDebugLine;

    [Range(0f, 1f)]
    public float m_pourcentdltl_Left;
    [Range(0f, 1f)]
    public float m_pourcentdldr_Down;
    [Range(0f, 1f)]
    public float m_pourcentdrtr_Right;
    [Range(0f, 1f)]
    public float m_pourcenttltr_Top;

    void Update()
    {
        m_localPlayZone.WorldToLocal(m_target, out Vector3 localWorld, out Vector3 local);
        if(m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.red,
            m_localPlayZone.m_dlLocalWorld, localWorld);
        if (m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.red,
           Vector3.zero, local);
        // MEASURE LEFT BORDER
        m_pourcentdltl_Left = local.z / m_localPlayZone.m_tlLocal.magnitude;
        //MEASURE DOWN BORDER
        m_pourcentdldr_Down = ComputePourcentOfSegment(Vector3.zero, m_localPlayZone.m_drLocal, local);
        //MEASURE TOP BORDER
        m_pourcenttltr_Top = ComputePourcentOfSegment(m_localPlayZone.m_tlLocal, m_localPlayZone.m_trLocal, local);
        //MEASURE RIGHT BORDER
        m_pourcentdrtr_Right = ComputePourcentOfSegment(m_localPlayZone.m_drLocal, m_localPlayZone.m_trLocal, local);

    }

    private float ComputePourcentOfSegment(Vector3 from, Vector3 to,
        Vector3 localPointToTrack )
    { 
        Vector3 relocateSegDirection = to - from;
        Quaternion relocatingRotation = Quaternion.Inverse(Quaternion.LookRotation(relocateSegDirection));
        Vector3 relocatedPoint = relocatingRotation*( localPointToTrack - from);
        if (m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.yellow,
           Vector3.zero, relocatedPoint);
        return relocatedPoint.z / relocateSegDirection.magnitude;
    }
}

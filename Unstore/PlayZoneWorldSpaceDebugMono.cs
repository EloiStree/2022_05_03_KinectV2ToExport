using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneWorldSpaceDebugMono : MonoBehaviour
{
    public KinectFourPointsSquareCalibrationZoneMono m_source;
    public Transform m_root;
    public bool m_removeHeight;
    public bool m_useDebugLine;

    [Header("Debug")]
    public Transform m_downLeftTransform;


    public Vector3 m_tlLocalWorld;
    public Vector3 m_trLocalWorld;
    public Vector3 m_dlLocalWorld;
    public Vector3 m_drLocalWorld;
    public Vector3 m_cameraLocalWorld;
    public Vector3 m_tlLocal;
    public Vector3 m_trLocal;
    public Vector3 m_dlLocal;
    public Vector3 m_drLocal;
    public Vector3 m_cameraLocal;
    public Quaternion m_downToTopLeftRotation;

    public void WorldToLocal(Transform point,out Vector3 worldLocal, out Vector3 local)
    {
        WorldToLocal(point.position, out worldLocal, out local);
    }
    public void WorldToLocal(Vector3 point, out Vector3 worldLocal, out Vector3 local)
    {
        local = m_downLeftTransform.InverseTransformPoint(point);
        if (m_removeHeight)
            local.y = 0;
        GetWorldPositionFromLocal(local, out worldLocal);
    }

    public void Update()
    {
        RefreshLeftDownInfo();
        WorldToLocal(m_source.m_topLeft, out m_tlLocalWorld, out m_tlLocal);
        WorldToLocal(m_source.m_topRight, out m_trLocalWorld, out m_trLocal);
        WorldToLocal(m_source.m_downRight, out m_drLocalWorld, out m_drLocal);
        WorldToLocal(m_source.m_downLeft, out m_dlLocalWorld, out m_dlLocal);
        WorldToLocal(m_source.m_kinectUsedCamera.transform, out m_cameraLocalWorld , out m_cameraLocal);


        if(m_useDebugLine)
        Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.green,
            m_tlLocalWorld, m_trLocalWorld,      
            m_drLocalWorld, m_dlLocalWorld, m_tlLocalWorld);
        if (m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.blue,
           m_dlLocalWorld, m_cameraLocalWorld, m_drLocalWorld, m_cameraLocalWorld
           ,
           m_tlLocalWorld, m_cameraLocalWorld, m_trLocalWorld, m_cameraLocalWorld);


        if (m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.green,
      m_tlLocal, m_trLocal,   m_drLocal, m_dlLocal, m_tlLocal);

        if (m_useDebugLine)
            Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.blue,
           m_dlLocal, m_cameraLocal, m_drLocal, m_cameraLocal
           ,
           m_tlLocal, m_cameraLocal, m_trLocal, m_cameraLocal);

    }


    private void RefreshLeftDownInfo()
    {
        m_downLeftTransform = m_source.m_downLeft;
        m_downToTopLeftRotation = Quaternion.LookRotation(m_source.m_topLeft.position - m_source.m_downLeft.position, m_source.m_direction);
        m_downLeftTransform.rotation = m_downToTopLeftRotation;
    }

    public void GetWorldPositionFromLocal(Vector3 local, out Vector3 world) {
       world= m_root.TransformPoint(local);
    }


    private void Reset()
    {
        m_root = transform;
    }
}

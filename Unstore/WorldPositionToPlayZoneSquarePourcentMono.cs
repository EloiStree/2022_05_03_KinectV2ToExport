using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionToPlayZoneSquarePourcentMono : MonoBehaviour
{

    public PlayZoneWorldSpaceDebugMono m_localPlayZone;

    public void GetPercent(Transform point, bool inverseHorizontal, out float horizontalPercent, out float verticalPercent)
    {
        GetPercent(point.position,inverseHorizontal, out horizontalPercent, out verticalPercent);
    }
        
    public void GetPercent(Vector3 point,bool inverseHorizontal,  out float horizontalPercent, out float verticalPercent)
    {
        Eloi.E_CodeTag.DirtyCode.Info("This code is unperfect but I don't have time to make it good enought");
        m_localPlayZone.WorldToLocal(point, out Vector3 localWorld, out Vector3 local);
        //Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.red,
        //    m_localPlayZone.m_dlLocalWorld, localWorld);
        //Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.red,
        //   Vector3.zero, local);
        // MEASURE LEFT BORDER
        verticalPercent = local.z / m_localPlayZone.m_tlLocal.magnitude;

        if (inverseHorizontal) { 
            //MEASURE DOWN BORDER
            horizontalPercent = ComputePourcentOfSegment( m_localPlayZone.m_drLocal,Vector3.zero, local);
        }
        else horizontalPercent = ComputePourcentOfSegment( Vector3.zero, m_localPlayZone.m_drLocal, local);
    }

    private float ComputePourcentOfSegment(Vector3 from, Vector3 to,
        Vector3 localPointToTrack)
    {
        Vector3 relocateSegDirection = to - from;
        Quaternion relocatingRotation = Quaternion.Inverse(Quaternion.LookRotation(relocateSegDirection));
        Vector3 relocatedPoint = relocatingRotation * (localPointToTrack - from);
        Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.yellow,
           Vector3.zero, relocatedPoint);
        return relocatedPoint.z / relocateSegDirection.magnitude;
    }
}

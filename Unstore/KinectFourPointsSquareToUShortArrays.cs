using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectFourPointsSquareToUShortArrays : MonoBehaviour
{
    public KinectFourPointsSquareCalibrationZoneMono m_source;
    public GetMesh3DWorldPointFromCameraMono m_cameraToWorld3D;
    public KinectManager m_kinectManager;
    public ushort[] m_playZonerFilterDepth;
    public KinectUshortDepthRefEvent m_refreshUshort;

    public ushort m_topLeft;
    public ushort m_topRight;
    public ushort m_downLeft;
    public ushort m_downRight;

    public KinectDepthScreenValue m_topLeftDepthCoordinate;
    public KinectDepthScreenValue m_topRightDepthCoordinate;
    public KinectDepthScreenValue m_downLeftDepthCoordinate;
    public KinectDepthScreenValue m_downRightDepthCoordinate;


    [ContextMenu("Refresh")]
    public void Refresh() {

        m_topLeftDepthCoordinate.m_screenValueX = (int)m_source.m_topLeftScreenPosition.x;
        m_topLeftDepthCoordinate.m_screenValueY = (int)m_source.m_topLeftScreenPosition.y;

        m_topRightDepthCoordinate.m_screenValueX = (int)m_source.m_topRightScreenPosition.x;
        m_topRightDepthCoordinate.m_screenValueY = (int)m_source.m_topRightScreenPosition.y;


        m_downLeftDepthCoordinate.m_screenValueX = (int)m_source.m_downLeftScreenPosition.x;
        m_downLeftDepthCoordinate.m_screenValueY = (int)m_source.m_downLeftScreenPosition.y;

        m_downRightDepthCoordinate.m_screenValueX = (int)m_source.m_downRighScreenPosition.x;
        m_downRightDepthCoordinate.m_screenValueY = (int)m_source.m_downRighScreenPosition.y;

        SetDepthCoordinateFromScreenValue(ref m_topLeftDepthCoordinate);
        SetDepthCoordinateFromScreenValue(ref m_topRightDepthCoordinate);
        SetDepthCoordinateFromScreenValue(ref m_downLeftDepthCoordinate);
        SetDepthCoordinateFromScreenValue(ref m_downRightDepthCoordinate);

        m_topLeft = GetDepthOf(in m_topLeftDepthCoordinate);
        m_topRight = GetDepthOf(in m_topRightDepthCoordinate); 
        m_downLeft = GetDepthOf(in m_downLeftDepthCoordinate); 
        m_downRight = GetDepthOf(in m_downRightDepthCoordinate); 


    }

    private ushort GetDepthOf(in KinectDepthScreenValue coordinate)
    {
        return m_kinectManager.GetDepthForPixel(coordinate.m_kinectDepthX, coordinate.m_kinectDepthY);
    }

    private void SetDepthCoordinateFromScreenValue(ref KinectDepthScreenValue coordinate)
    {
        m_cameraToWorld3D.GetScreenPixelMaxInfo(new Vector2(coordinate.m_screenValueX, coordinate.m_screenValueY)
            , out coordinate.m_kinectDepthX, out coordinate.m_kinectDepthY,
            out ushort info, out Vector3 worldPos );
    }
}
[System.Serializable]
public class KinectDepthScreenValue
{
    public int m_screenValueX;
    public int m_screenValueY;
    public int m_kinectDepthX;
    public int m_kinectDepthY;
}

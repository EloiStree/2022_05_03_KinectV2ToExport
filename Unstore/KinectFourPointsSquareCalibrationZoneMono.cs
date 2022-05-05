using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectFourPointsSquareCalibrationZoneMono : MonoBehaviour
{
    public float m_minHeightToBeConsiderCm = 0.1f;
    public Camera m_kinectUsedCamera;
    public Transform m_originKinectRoot;
    public Transform m_topLeft;
    public Transform m_topRight;
    public Transform m_downLeft;
    public Transform m_downRight;

    public Vector3 m_topLeftLocalPosition;
    public Vector3 m_topRightLocalPosition;
    public Vector3 m_downLeftLocalPosition;
    public Vector3 m_downRightLocalPosition;

    public Vector2 m_topLeftScreenPosition;
    public Vector2 m_topRightScreenPosition;
    public Vector2 m_downLeftScreenPosition;
    public Vector2 m_downRighScreenPosition;


    public Vector3 m_direction;


    public void Update()
    {

        m_direction = Vector3.Cross(m_topRight.position - m_downLeft.position, m_topLeft.position - m_downRight.position) * -1f;

        m_topLeftLocalPosition = m_originKinectRoot.InverseTransformPoint(m_topLeft.position);
        m_topRightLocalPosition = m_originKinectRoot.InverseTransformPoint(m_topRight.position);
        m_downLeftLocalPosition = m_originKinectRoot.InverseTransformPoint(m_downLeft.position);
        m_downRightLocalPosition = m_originKinectRoot.InverseTransformPoint(m_downRight.position);

        m_topLeftScreenPosition = m_kinectUsedCamera.WorldToScreenPoint(m_topLeft.position);
        m_topRightScreenPosition = m_kinectUsedCamera.WorldToScreenPoint(m_topRight.position);
        m_downLeftScreenPosition = m_kinectUsedCamera.WorldToScreenPoint(m_downLeft.position);
        m_downRighScreenPosition = m_kinectUsedCamera.WorldToScreenPoint(m_downRight.position);


        Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.green, m_topLeft.position, m_topRight.position, m_downRight.position, m_downLeft.position, m_topLeft.position);
        Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.green, m_topLeft.position, m_downRight.position);
        Eloi.E_DrawingUtility.DrawLines(Time.deltaTime, Color.green, m_downLeft.position, m_topRight.position);

        Debug.DrawLine(m_topLeft.position, m_topLeft.position + m_direction * m_minHeightToBeConsiderCm, Color.blue);
        Debug.DrawLine(m_topRight.position, m_topRight.position + m_direction * m_minHeightToBeConsiderCm, Color.blue);
        Debug.DrawLine(m_downLeft.position, m_downLeft.position + m_direction * m_minHeightToBeConsiderCm, Color.blue);
        Debug.DrawLine(m_downRight.position, m_downRight.position + m_direction * m_minHeightToBeConsiderCm, Color.blue);

   

    }
}

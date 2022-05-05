using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_GetMesh3DWorldPointFromCameraMono : MonoBehaviour
{
    public GetMesh3DWorldPointFromCameraMono m_source;

    public Transform[] m_squarePlayZoneCorner;
    public int m_index;


    public void AnchorAndGoNext() {

        Transform toMove = m_squarePlayZoneCorner[m_index];
        m_index++;
        if (m_index >= m_squarePlayZoneCorner.Length)
            m_index = 0;

        Vector3 mousePosition = Input.mousePosition;
         m_source.GetMeshWorldPointOfScreenPoint( (int)mousePosition.x, (int) mousePosition.y, out Vector3 position);
        toMove.position = position;

    }

}

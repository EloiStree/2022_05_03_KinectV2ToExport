using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobDetectorToCubeV0Mono : MonoBehaviour
{
    public BlobDetectorToCubeInLocalSpace m_source;
    public Transform m_root;
    public Transform[] m_cubes;
    public TestAround[] m_testAround;

    public Transform m_zoneCube;

    public KinectUShortDepthRef m_depthFiltered;
    public List<Blob> m_blobs;
    Vector3 [] m_positionPerDepthPixel;
    public int m_depthWidth;
    public int m_depthHeight;


    [System.Serializable]
    public class TestAround
    {
        public bool isvalide;
        public Vector3 botLeftNear;
        public Vector3 topRightFar;

    }


    public void Update()
    {

        RefreshView();
    }

    public void RefreshView()
    {
        m_depthWidth = m_source.GetDepthWidth();
        m_depthHeight = m_source.GetDepthHeight();
        m_blobs = m_source.blobs;
        m_positionPerDepthPixel = m_source.kinectManager.GetSensorData().depth2SpaceCoords;
        m_depthFiltered = m_source.m_depthRefFiltered;

        if (m_depthFiltered == null || m_positionPerDepthPixel == null || m_blobs == null)
            return;

        DrawKinectZone();

        DrawCubes();
    }
    public float m_maxDepth =10;
    public string test = "";



    private void DrawKinectZone()
    {
        Vector3 localScale = new Vector3(0.01f,0.01f, m_maxDepth);
        Vector3 localsPosition = new Vector3(0, 0, m_maxDepth/2f);
        m_zoneCube.parent = m_root;
        m_zoneCube.localPosition = localsPosition;
        m_zoneCube.localScale = localScale;

        //m_topLeft = m_positionPerDepthPixel     [Get2DTO1D(0, 0, in m_depthWidth)               ];
        //m_topRight = m_positionPerDepthPixel    [Get2DTO1D(m_depthWidth - 1, 0, in m_depthWidth)    ];
        //m_downLeft = m_positionPerDepthPixel    [Get2DTO1D(0, m_depthHeight-1, in m_depthWidth) ];
        //m_downRight = m_positionPerDepthPixel   [Get2DTO1D(m_depthWidth - 1, m_depthHeight - 1, in m_depthWidth)];
        //test = "";
        //test += " " + Get2DTO1D(0, 0, in m_depthWidth)               ;
        //test += " " + Get2DTO1D(m_depthWidth - 1, 0, in m_depthWidth)    ;
        //test += " " + Get2DTO1D(0, m_depthHeight-1, in m_depthWidth) ;
        //test += " " + Get2DTO1D(m_depthWidth - 1, m_depthHeight - 1, in m_depthWidth) ;

      
        //if (m_startPoint.magnitude < 100 && m_endPoint.magnitude < 100) {

        //    Debug.DrawLine(m_startPoint, m_endPoint, Color.red,500);
        //}


    }

    //public Vector3 m_topLeft;
    //public Vector3 m_topRight;
    //public Vector3 m_downLeft;
    //public Vector3 m_downRight;
    //public Vector3 m_startPoint;

    public int Get2DTO1D(in int x, in int y, in int width)
    {
        return x + y * width;
    }
    public void Get1DTO2D(in int index,in int width, out int x, out int y)
    {
        x = (int) (index % width);
        y = (int) (index / width);
    }

    private void DrawCubes()
    {
        for (int i = 0; i < m_cubes.Length; i++)
        {
            m_cubes[i].gameObject.SetActive(i < m_blobs.Count);
            if (i < m_blobs.Count)
            {
                UpdateCube(i);
            }
            else
            {
                UpdateCubeToZero(i);
            }

        }
    }

    private void UpdateCubeToZero(int i)
    {
        m_cubes[i].localPosition = Vector3.zero;
        m_cubes[i].localScale = Vector3.zero;
    }

    private void UpdateCube(int i)
    {
        Vector3 botLeftNearPoint;
        Vector3 topRightFarPoint;
        FetchCubeCorner(i,out bool found, out botLeftNearPoint, out topRightFarPoint);

        if (found)
        {
            m_cubes[i].localPosition = (botLeftNearPoint + topRightFarPoint) / 2f;
            m_cubes[i].localScale = (topRightFarPoint - botLeftNearPoint) / 2f;
        }
        else
        {
            m_cubes[i].localPosition = Vector3.zero;
            m_cubes[i].localScale = Vector3.zero;
        }
        if (found)
            Debug.DrawLine(botLeftNearPoint, topRightFarPoint, Color.red, 0.5f);
    }

    private void FetchCubeCorner(int i, out bool found, out Vector3 botLeftNearPoint, out Vector3 topRightFarPoint)
    {
        Blob b = m_blobs[i];
        botLeftNearPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        topRightFarPoint = new Vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);
       // Vector3 positionOfPoint;

        for (int x = b.minx; x < b.maxx; x++)
        {
            for (int y = b.miny; y < b.maxy; y++)
            {
                int di = Get2DTO1D(in x, in y, m_depthWidth);

                //From arry of ushort
                //positionOfPoint = m_positionPerDepthPixel[di];
                //from access by single
                //positionOfPoint = m_source.kinectManager.GetDepthForPixel(x, y);
                if (m_positionPerDepthPixel[di].magnitude < float.MaxValue*0.9f) {
                    if (m_positionPerDepthPixel[di].x < botLeftNearPoint.x)
                    {
                        botLeftNearPoint.x = m_positionPerDepthPixel[di].x;
                    }
                    if (m_positionPerDepthPixel[di].x > topRightFarPoint.x)
                    {
                        topRightFarPoint.x = m_positionPerDepthPixel[di].x;
                    }

                    if (m_positionPerDepthPixel[di].y < botLeftNearPoint.y)
                    {
                        botLeftNearPoint.y = m_positionPerDepthPixel[di].y;
                    }
                    if (m_positionPerDepthPixel[di].y > topRightFarPoint.y)
                    {
                        topRightFarPoint.y = m_positionPerDepthPixel[di].y;
                    }

                    if (m_positionPerDepthPixel[di].z < botLeftNearPoint.z)
                    {
                        botLeftNearPoint.z = m_positionPerDepthPixel[di].z;
                    }
                    if (m_positionPerDepthPixel[di].z > topRightFarPoint.z)
                    {
                        topRightFarPoint.z = m_positionPerDepthPixel[di].z;
                    }
                }
            }
        }
        Debug.DrawLine(botLeftNearPoint, topRightFarPoint, Color.red, 0.5f);
        found = botLeftNearPoint.magnitude < float.MaxValue && topRightFarPoint.magnitude < float.MaxValue;

        m_testAround[i].isvalide = found;
        m_testAround[i].botLeftNear = botLeftNearPoint;
        m_testAround[i].topRightFar = topRightFarPoint;
    }



    //// instantiates representative blob objects for each blob
    //private void InstantiateBlobObjects()
    //{

    //    if (kinectManager.GetSensorData().depth2SpaceCoords == null)
    //    {
    //        kinectManager.GetSensorData().depth2SpaceCoords = new Vector3[kinectManager.GetDepthImageWidth() * kinectManager.GetDepthImageHeight()];
    //    }

    //    int bi = 0;
    //    foreach (var b in blobs)
    //    {
    //        KinectBlobSourceRefMono refBlob = null;
    //        while (bi >= blobObjects.Count)
    //        {
    //            var cub = Instantiate(blobPrefab, new Vector3(0, 0, -10), Quaternion.identity);
    //            //cub.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);  // to match the dimensions of a ball

    //            blobObjects.Add(cub);
    //            cub.transform.parent = blobsRootObj.transform;
    //            refBlob = cub.AddComponent<KinectBlobSourceRefMono>();
    //            refBlob.SetInfo(b, m_depthRefFilter);


    //            cub = Instantiate(blobPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    //            //cub.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);  // to match the dimensions of a ball
    //            m_createBlobPosition.Add(cub);
    //            cub.transform.parent = m_kinectBlobRoot.transform;
    //            refBlob = cub.AddComponent<KinectBlobSourceRefMono>();
    //            refBlob.SetInfo(b, m_depthRefFilter);

    //        }

    //        Vector3 blobCenter = b.GetBlobCenter();
    //        Vector3 blobSpaceOverlayerPos = kinectManager.GetPosDepthOverlay((int)blobCenter.x, (int)blobCenter.y, (ushort)blobCenter.z, foregroundCamera, foregroundImgRect);
    //        //Vector3 blobSpacePos = kinectManager.getdepth((int)blobCenter.x, (int)blobCenter.y);
    //        //Vector3 blobSpacePos = kinectManager.GetDepthForPixel((int)blobCenter.x, (int)blobCenter.y);
    //        //kinectManager.depth
    //        blobObjects[bi].name = "Blob" + bi;

    //        refBlob = blobObjects[bi].GetComponent<KinectBlobSourceRefMono>();
    //        refBlob.SetInfo(b);
    //        refBlob = m_createBlobPosition[bi].GetComponent<KinectBlobSourceRefMono>();
    //        refBlob.SetInfo(b);
    //        int d1Center = ((int)blobCenter.x) + (((int)blobCenter.y) * refBlob.m_blobDetphRef.m_width);

    //        if (kinectManager != null && kinectManager.GetSensorData() != null && kinectManager.GetSensorData().depth2SpaceCoords != null)
    //        {
    //            Vector3 p = kinectManager.GetSensorData().depth2SpaceCoords[d1Center];
    //            if (p.magnitude < 15)
    //                m_createBlobPosition[bi].transform.localPosition = p;
    //            else
    //                m_createBlobPosition[bi].transform.localPosition = Vector3.zero;

    //        }


    //        bi++;
    //    }

    //    // remove the extra cubes
    //    for (int i = blobObjects.Count - 1; i >= bi; i--)
    //    {
    //        Destroy(blobObjects[i]);
    //        blobObjects.RemoveAt(i);
    //    }
    //    for (int i = m_createBlobPosition.Count - 1; i >= bi; i--)
    //    {
    //        Destroy(m_createBlobPosition[i]);
    //        m_createBlobPosition.RemoveAt(i);
    //    }
    //}
}

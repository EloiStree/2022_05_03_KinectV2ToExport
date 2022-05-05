using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectBlobSourceRefMono : MonoBehaviour
{
    public Blob m_blobInfo;
    public KinectUShortDepthRef m_blobDetphRef;


    public void SetInfo(Blob blob, KinectUShortDepthRef dephtRef) {

        m_blobDetphRef = dephtRef;
        m_blobInfo = blob;
    }

    public void SetInfo(Blob blob)
    {
        m_blobInfo = blob;
    }
}

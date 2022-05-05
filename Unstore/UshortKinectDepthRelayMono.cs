using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UshortKinectDepthRelayMono : MonoBehaviour
{

    public KinectUshortDepthRefEvent m_relay;

    public void PushIn(KinectUShortDepthRef toPush) {
        m_relay.Invoke(toPush);
    }
}

public class KinectUShortDepthRef {

    public int m_width;
    public int m_height;
    public ushort[] m_arrayRef;

    public KinectUShortDepthRef(int width, int height, int lenght)
    {
        m_width = width;
        m_height = height;
        m_arrayRef = new ushort[lenght];
    }
}

[System.Serializable]
public class KinectUshortDepthRefEvent :UnityEvent<KinectUShortDepthRef>{ 
}

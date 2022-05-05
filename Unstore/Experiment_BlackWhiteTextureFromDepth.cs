using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_BlackWhiteTextureFromDepth : MonoBehaviour
{
    public KinectUShortDepthRef m_kinectDepthUshort;
    public Texture2D m_texture;
    public float m_timeBetween = 0.1f;

    public int m_width;
    public int m_height;

    private Color[] m_colors;
    private Color m_black = Color.black;
    private Color m_white = Color.white;
    public void SetRef(KinectUShortDepthRef depthRef) {
        m_kinectDepthUshort = depthRef;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true) {

            if (m_kinectDepthUshort != null && m_kinectDepthUshort.m_arrayRef != null && m_kinectDepthUshort.m_arrayRef.Length > 0) {

                m_width = m_kinectDepthUshort.m_width;
                m_height = m_kinectDepthUshort.m_height;
                if (m_colors==null ||  m_colors.Length != m_kinectDepthUshort.m_arrayRef.Length) { 
                    m_colors = new Color[m_kinectDepthUshort.m_arrayRef.Length];
                    m_texture = new Texture2D(m_kinectDepthUshort.m_width, m_kinectDepthUshort.m_height);
                }

                if (m_texture != null) { 
                    for (int i = 0; i < m_kinectDepthUshort.m_arrayRef.Length; i++)
                    {
                        m_colors[i] = m_kinectDepthUshort.m_arrayRef[i] > 0 ? m_white : m_black;
                    }
                    m_texture.SetPixels(m_colors);
                    m_texture.Apply();
                }
            }
            yield return new WaitForSeconds(m_timeBetween);
            yield return new WaitForEndOfFrame();
        }
        
    }

}

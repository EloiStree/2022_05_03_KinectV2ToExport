using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectVerticalPitchAdjustmentMono : MonoBehaviour
{
    public float m_pitchAdjustement=0;
    public Transform m_toAffect;


    private void Start()
    {
        AdjustPivotPitchTo(m_pitchAdjustement);
    }

    private void AdjustPivotPitchTo(float pitchAdjustement)
    {
        m_toAffect.rotation = Quaternion.Euler(pitchAdjustement, 0, 0);
    }

    private void OnValidate()
    {

        AdjustPivotPitchTo(m_pitchAdjustement);
    }
}

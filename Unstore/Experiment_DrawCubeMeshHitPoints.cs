using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_DrawCubeMeshHitPoints : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            Debug.DrawLine(collision.contacts[i].point, collision.contacts[i].point + Vector3.forward * 0.05f, Color.blue + Color.green);
        }
    }
}

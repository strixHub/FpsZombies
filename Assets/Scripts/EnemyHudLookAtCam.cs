using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHudLookAtCam : MonoBehaviour
{
    public Camera lookAtCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = lookAtCamera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(lookAtCamera.transform.position - v);
        transform.Rotate(0,180,0);
    }
}

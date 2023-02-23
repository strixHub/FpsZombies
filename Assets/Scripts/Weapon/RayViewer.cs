using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    /*
    this script will show us the zombie info when aiming at it*/
    public float weaponRange = 50f;
    public Camera fpsCam;
    void Start()
    {
        //fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 shotOrigen = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
        Debug.DrawLine(shotOrigen,fpsCam.transform.forward * weaponRange, Color.green);
        RaycastHit hit;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject camera;
    
    [HideInInspector]
    public float targetXRotation, targetYRotation;
    [HideInInspector]
    public float targetXRotationV, targetYRotationV;

    public GameObject shell;
    public Transform shellSpawnPos, bulletSpawnPos;
    public float rotateSpeed = 0.3f, holdHeight = -.5f, holdSide = .5f;

    // Update is called once per frame
    void Update()
    {
        Shot();
        targetXRotation = Mathf.SmoothDamp(targetXRotation, FindObjectOfType<MouseLook>().xRot, ref targetXRotationV, rotateSpeed);
        targetYRotation = Mathf.SmoothDamp(targetYRotation, FindObjectOfType<MouseLook>().yRot, ref targetYRotationV, rotateSpeed);
        
        transform.position = camera.transform.position + Quaternion.Euler(0, targetYRotation, 0)* new Vector3(holdSide, holdHeight,0);

        float clampedX = Mathf.Clamp (targetXRotation, -70, 80);
        transform.rotation = Quaternion.Euler(-clampedX, targetYRotation, rotateSpeed);

    }

    void Shot()
    {
        if(Input.GetButtonDown("Fire1")){
            Fire();
        }
    }

    void Fire ()
    {
        GameObject shellCopy = Instantiate<GameObject> (shell, shellSpawnPos.position, Quaternion.identity) as GameObject;
        RaycastHit variable;
        bool status = Physics.Raycast (bulletSpawnPos.position, bulletSpawnPos.forward, out variable, 100);

        if(status)
        {
            Debug.Log(variable.collider.gameObject.name);
        }
    }
}

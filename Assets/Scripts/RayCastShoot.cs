using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    public int gunDmg = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    private Animator animator;
    public float hitForce = 0f;
    public Transform gunEnd;
    public Camera fpsCam;
    private WaitForSeconds shotDurations = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    //private LineRenderer shotLine;
    public ParticleSystem muzzleFlash;
    private float nextFire; 
    void Start()
    {
        //shotLine = GetComponent<LineRenderer>();
        animator = GetComponentInChildren<Animator>();
        gunAudio = GetComponentInChildren<AudioSource>();
        //fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time>nextFire){
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());

            Vector3 shotOrigen = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
            RaycastHit hit;

            //shotLine.SetPosition(0, gunEnd.position);

            if(Physics.Raycast(shotOrigen, fpsCam.transform.forward, out hit, weaponRange)){
                //Debug.Log("Hit");
                //shotLine.SetPosition(1, hit.point);
                EnemyControler health = hit.collider.GetComponent<EnemyControler>();
                if(health!= null){
                    health.getDmg(gunDmg);

                }

            }else{
                //no hit shotLine.SetPosition(1, shotOrigen + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect(){
        gunAudio.Play();
        animator.ResetTrigger("Shooting");
        animator.SetTrigger("Shooting");
        muzzleFlash.Play();
        //shotLine.enabled = true;
        yield return shotDurations;
        //shotLine.enabled = false;
    }
}

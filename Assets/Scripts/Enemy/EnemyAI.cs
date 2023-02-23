using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public float stopDistance =2.5f;
    NavMeshAgent nvm;
    private Animator animator;
    public Transform target; 
    // Start is called before the first frame update
    void Start()
    {
        nvm = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position,target.transform.position);
        if(dist<stopDistance){
            StopEnemy();
            ZombieAttack();
        }
        else{
            nvm.isStopped = false;
            nvm.SetDestination(target.position);
        }
        
    }
    void ZombieAttack(){
        animator.ResetTrigger("inRange");
        animator.SetTrigger("inRange");
        while(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            nvm.enabled  = false;
        }
        nvm.enabled = true;
        //activar collider de manos y si esos collider hacen daño esto, igual en otro script
        //target.GetComponent<PlayerHealth>().takeDmg(EnemyControler.dmg);
    }

    void StopEnemy(){
        nvm.enabled = false;
    }
}

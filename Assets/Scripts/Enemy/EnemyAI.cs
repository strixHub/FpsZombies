using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public float stopDistance =2.5f;
    NavMeshAgent nvm;
    public Transform target; 
    public PlayerHealth playerHP;
    // Start is called before the first frame update
    void Start()
    {
        nvm = GetComponent<NavMeshAgent>();
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
        
        playerHP.takeDmg(EnemyControler.dmg);
    }

    void StopEnemy(){
        nvm.isStopped = true;
    }
}

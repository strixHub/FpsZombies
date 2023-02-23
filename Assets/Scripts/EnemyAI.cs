using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{

    NavMeshAgent nvm;
    public Transform target; 
    // Start is called before the first frame update
    void Start()
    {
        nvm = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nvm.SetDestination(target.position);
        
    }
}

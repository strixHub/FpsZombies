using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombie;
    public static int nOfZombies = 0;
    public static int MAXZOMBIES = 24;
    private static int round = 1;
    
    private float startDelay = 1;
    private float spawnInterval;


    private void Start() {
        spawnInterval = Random.Range(3, 5);
        Invoke("SpawnZombie", startDelay);
    }

    private void SpawnZombie(){
        
        if(nOfZombies < MAXZOMBIES){
            nOfZombies++;
            Instantiate(zombie, transform.position, transform.rotation);
            
            spawnInterval = Random.Range(3, 5);
        }
        
        Invoke("SpawnZombie", spawnInterval);
    }

}

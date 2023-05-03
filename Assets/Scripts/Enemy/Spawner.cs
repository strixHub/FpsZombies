using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
            GameObject z = Instantiate(zombie, transform.position, transform.rotation);
            z.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text = zombie.GetComponent<EnemyControler>().word;
            spawnInterval = Random.Range(3, 5);
        }
        
        Invoke("SpawnZombie", spawnInterval);
    }

}

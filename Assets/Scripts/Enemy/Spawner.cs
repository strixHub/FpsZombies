using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombie;
    public static int nOfZombies = 0;
    public static int MAXZOMBIES = 24;
    private static int round = 1;


    private void Start() {
        
    }

    private void Update() {
        if(nOfZombies < MAXZOMBIES){
            nOfZombies++;
            Instantiate(zombie);
        }
    }

}

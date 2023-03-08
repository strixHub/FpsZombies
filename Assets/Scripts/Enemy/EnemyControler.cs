using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControler : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;

    public static int dmg = 30;
    //falta el caracter a aprender
    public Image frontSTB;
    public Image backSTB;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        updateZombiUI();
    }

    public void getDmg(int dmg){
        
        currentHealth -= dmg;
        Debug.Log(currentHealth);
        updateZombiUI();
        if(currentHealth<=0){
            //die animation and despawn
            gameObject.SetActive(false);
            //death anim for 5 seconds, then destroy
            //Destroy(gameObject);
            Spawner.nOfZombies --;
        }
    }

    void updateZombiUI()
    {
        float timer = 1f;
        float cheapSpeed = .2f;
        float fillF = frontSTB.fillAmount;
        float fillB = backSTB.fillAmount;
        float healthFraction = currentHealth/maxHealth;

        frontSTB.fillAmount = healthFraction;
        backSTB.color = Color.black;
        timer += Time.deltaTime;
        float percent = timer/cheapSpeed;

        backSTB.fillAmount = Mathf.Lerp(fillB, healthFraction, percent);
    }
}

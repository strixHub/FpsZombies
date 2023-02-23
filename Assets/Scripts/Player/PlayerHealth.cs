using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    public float cheapSpeed = 2f;
    public Image frontHB;
    public Image backHB;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        updateHealthUI();        
        //if(){
        //    takeDmg(20f);
        //}
    }
    public void updateHealthUI(){
        float fillF = frontHB.fillAmount;
        float fillB = backHB.fillAmount;
        float healthFraction = health/maxHealth;
        if(fillB>healthFraction){ //take damage
            frontHB.fillAmount = healthFraction;
            backHB.color = Color.red;
            timer += Time.deltaTime;
            float percent = timer/cheapSpeed;
            percent = percent * percent;//better animation
            backHB.fillAmount = Mathf.Lerp(fillB, healthFraction, percent);
        }

        if(fillF<healthFraction){ //heal
            backHB.color = Color.green;
            timer += Time.deltaTime;
            float percent = timer/cheapSpeed;
            percent = percent * percent;
            frontHB.fillAmount = Mathf.Lerp(fillF, backHB.fillAmount, percent);
        }
    }

    public void takeDmg(float dmg){
        health-=dmg;
        timer = 0f;
    }

    public void restoreHealth(float heal){
        
        health+=heal;
        timer = 0f;
    }
}

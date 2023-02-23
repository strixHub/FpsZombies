using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    private float stamina;
    public float cheapSpeed = 2f;
    public float fatigueTimer = 0f;
    public Image frontSTB;
    public Image backSTB;
    private float timer;
    PlayerMotor pm;
    
    private PlayerInput.OnFootActions onFoot;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        updateStaminaUI();   
        
        if(Mathf.Abs(rb.velocity.magnitude)>=8){
            loseStamina(0.3f);
        }else{
            healStamina(0.4f);
        }
    }

    void updateStaminaUI(){
        float fillF = frontSTB.fillAmount;
        float fillB = backSTB.fillAmount;
        float staminaFraction = stamina/maxStamina;
        if(fillB>staminaFraction){ //take damage//if sprinting
            frontSTB.fillAmount = staminaFraction;
            backSTB.color = Color.green;
            timer += Time.deltaTime;
            float percent = timer/cheapSpeed;
            backSTB.fillAmount = Mathf.Lerp(fillB, staminaFraction, percent);
        }
        if(fillF<staminaFraction){ //recover stamina
            backSTB.color = Color.green;
            timer += Time.deltaTime;
            float percent = timer/cheapSpeed;
            frontSTB.fillAmount = Mathf.Lerp(fillF, staminaFraction, stamina);
        }
        if(fillF>fillB){
            backSTB.fillAmount = frontSTB.fillAmount;
        }
    }


    public void loseStamina(float stLoss){
        stamina-=stLoss;
        timer = 0f;
        if(stamina <= 0f){
            fatigueTimer = 100;
            pm.setTired(true);
        }
    }

    public void healStamina(float stHeal){
        stamina+=stHeal;
        timer = 0f;
        fatigueTimer --;
         if (fatigueTimer >= 3)
        {
            fatigueTimer = 0;
            pm.setTired(false);
        }
    }
    
}

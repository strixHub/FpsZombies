using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public string word;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        var random = new System.Random();
        int rndIndex = random.Next(GameMng.SortedList.Count);
        word = GameMng.SortedList[rndIndex].word;
        gameObject.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text = word;

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
            //Debug.Log(gameObject.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text);
            Debug.Log(Spawner.nOfZombies);
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

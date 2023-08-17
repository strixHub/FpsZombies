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
    public WordClass wordClass;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        var random = new System.Random();
        int rndIndex = random.Next(GameMng.SortedList.Count);
        word = GameMng.SortedList[rndIndex].word;
        gameObject.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text = word;
        wordClass = GameMng.SortedList[rndIndex];

    }

    // Update is called once per frame
    void Update()
    {
        updateZombiUI();
    }

    public void getDmg(int dmg, PlayerObjective po){
        
        currentHealth -= dmg;
        updateZombiUI();
        if(currentHealth<=0){
            gameObject.SetActive(false);
            po.ChangeObjective();
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

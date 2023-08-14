using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
   
   public PlayerHealth player;
   public GameObject playerEntity;
   public GameObject childObject;
   public PlayerLook lookScript;
   public float cameraAnimSpeed = 3.0f;
   private bool showGUI = false;
   private bool canReloadLevel = false;
   public Texture deathTexture;
   private Color guiColor = Color.white;

   void Start(){
    player.OnDeath += ShowDeathAnim;
   }

   void ShowDeathAnim(){
        player.OnDeath -= ShowDeathAnim;
        StartCoroutine(MoveThroughAnim());
   }
   private IEnumerator MoveThroughAnim(){
        transform.parent = null;
        childObject.SetActive(false);
        //todo camerapath.transform.parent = null;
        lookScript.enabled = false;
        float current = 0.0f;
        float deltaTime = 1.0f/cameraAnimSpeed;

        while(current<1){
            yield return null;
            current += Time.deltaTime * deltaTime;
            //TODO transform.position = cameraPath.GetPointAtTime(current);
            transform.LookAt(playerEntity.transform);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInGUI());
   }

    void DeactivateMonoBehaviours(GameObject obj){
        foreach(MonoBehaviour bh in obj.GetComponentsInChildren<MonoBehaviour>()){
            bh.enabled = false;
        }
    }

   private IEnumerator FadeInGUI(){
        guiColor.a = 0.0f;
        showGUI = true;
        while(guiColor.a <1){
            yield return null;
            guiColor.a += Time.deltaTime;
        }
        canReloadLevel = true;
   }

    private void OnGUI() {
        if(showGUI){
            GUI.color = guiColor;
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), deathTexture);
            if(canReloadLevel){
                if(Event.current.type == EventType.KeyUp){
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
        
    }
}

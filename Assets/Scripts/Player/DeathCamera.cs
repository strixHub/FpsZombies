using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
   private static float startVolume;
   private static float currentVolume;
   private Color guiColor = Color.white;

   void Start(){
    player.OnDeath += ShowDeathAnim;
   }

   void ShowDeathAnim(){
        player.OnDeath -= ShowDeathAnim;
        StartCoroutine(MoveThroughAnim());
   }
   private IEnumerator MoveThroughAnim(){
        
        playerEntity.GetComponent<PlayerMotor>().speed = 2.5f;
        playerEntity.GetComponent<PlayerMotor>().setTired(true);
        yield return new WaitForSeconds(1f);
        
        IEnumerator fadeSound1 = FadeOutAudio (1f);
        StartCoroutine (fadeSound1);
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

     public static IEnumerator FadeOutAudio (float FadeTime) {
        startVolume = AudioListener.volume;
 
        while (AudioListener.volume > 0) {
            AudioListener.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
    }
    private void OnGUI() {
        if(showGUI){
            GUI.color = guiColor;
            
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), deathTexture);
            if(canReloadLevel){
                if(Event.current.type == EventType.KeyUp){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
        }
        
    }
}

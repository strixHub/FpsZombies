using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    Resolution [] resolutions;
    public static TMP_Dropdown resDropdown;

    public static Slider volume;
    public static AudioMixer audio;
    public static TMP_Dropdown graphics;
    public static Toggle fullScreenChk;
    
    private void Start() {
        resolutions = Screen.resolutions; 
        resDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;

        for(int i = 0; i<resolutions.Length; i++ ){
            options.Add(resolutions[i].width+" x "+resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz");
            
            if(PlayerPrefs.GetInt("Width", -2)== -2){

                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height 
                && resolutions[i].refreshRate == Screen.currentResolution.refreshRate){
                    currentResIndex = i;
                }
            }else{
                if(resolutions[i].width == PlayerPrefs.GetInt("Width") && PlayerPrefs.GetInt("Height") == resolutions[i].height
                && PlayerPrefs.GetInt("RefreshRate") == resolutions[i].refreshRate){
                    currentResIndex = i;
                }
            }
        }
        resDropdown.AddOptions(options);   
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume){
        audio.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int quality){
        QualitySettings.SetQualityLevel(quality);
        PlayerPrefs.SetInt("Quality", quality);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool full){
        Screen.fullScreen = full;
        int scrFull = full==true?1:0;
        PlayerPrefs.SetInt("Fullscreen",1);
        
        PlayerPrefs.Save();
    }

    public void SetResolution(int index){
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
        
        PlayerPrefs.SetInt("Width",res.width);
        PlayerPrefs.SetInt("Height",res.height);
        PlayerPrefs.SetInt("RefreshRate",res.refreshRate);
        
        PlayerPrefs.Save();
    }

    public static void LoadSavedConfig(){
        
        if(PlayerPrefs.GetFloat("MasterVolume",-2) != -2){
            volume.value = PlayerPrefs.GetFloat("MasterVolume");        
            audio.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));

        }
        
        if(PlayerPrefs.GetInt("Quality",-2) != -2){
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
            graphics.SetValueWithoutNotify(PlayerPrefs.GetInt("Quality"));
        }

        if(PlayerPrefs.GetInt("Fullscreen",-2) != -2){
            bool screenSts = PlayerPrefs.GetInt("Fullscreen")==1?true:false;
            Screen.fullScreen = screenSts;
            fullScreenChk.isOn = screenSts;
        }

        if(PlayerPrefs.GetInt("Width",-2)!=-2){
            Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), Screen.fullScreen, 
            PlayerPrefs.GetInt("RefreshRate"));
        }
    }
}

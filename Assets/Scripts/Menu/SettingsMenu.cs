using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    Resolution [] resolutions;
    public TMPro.TMP_Dropdown resDropdown;
    private void Start() {
        resolutions = Screen.resolutions; 
        resDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;

        for(int i = 0; i<resolutions.Length; i++ ){
            options.Add(resolutions[i].width+" x "+resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz");

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height 
            && resolutions[i].refreshRate == Screen.currentResolution.refreshRate){
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);   
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }
    public AudioMixer audio;
    public void SetVolume(float volume){
        audio.SetFloat("MasterVolume", volume);
    }

    public void SetQuality(int quality){
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetFullscreen(bool full){
        Screen.fullScreen = full;
    }

    public void SetResolution(int index){
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
    }
}

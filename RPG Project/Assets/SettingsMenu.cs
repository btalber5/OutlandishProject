using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{


    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        Debug.Log(resolutions.Length);


            int currenResIndex = 0;
        for (int i = 0; i < resolutions.Length;i++) {

            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {

                currenResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currenResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetQuality(int index) {

    
    

        QualitySettings.SetQualityLevel(index);

    }

    public void SetFullScreen(bool isFullscreen) {


        Screen.fullScreen = isFullscreen;
    
    }

    public void UpdateRes(int resIndex) {

        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    
    }
}

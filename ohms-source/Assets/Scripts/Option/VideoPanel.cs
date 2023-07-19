using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Screen = UnityEngine.Screen;

public class VideoPanel : MonoBehaviour
{
    public TMP_Dropdown resolutionTarget;
    public TMP_Dropdown screenModeTarget;
    private UnityEngine.Resolution[] resArr;
    OptionController optionController;

    void LoadResolution()
    {
        resolutionTarget.ClearOptions();
        resArr = Screen.resolutions;
        int idx = 0;
        foreach(UnityEngine.Resolution res in resArr)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = string.Format("{0} x {1}", res.width, res.height);
            resolutionTarget.options.Add(data);
            
            if(data.text == optionController.currentOption.video.resolution)
            {
                resolutionTarget.value = idx;
            }
            idx++;
        }
    }

    void LoadScreenMode()
    {
        int idx = 0;
        foreach(TMP_Dropdown.OptionData data in screenModeTarget.options)
        {
            if(data.text == optionController.currentOption.video.screenMode)
                screenModeTarget.value = idx;
            idx++;
        }
    }

    void Start()
    {
        optionController = FindObjectOfType<OptionController>();
        LoadResolution();
        LoadScreenMode();
    }
}

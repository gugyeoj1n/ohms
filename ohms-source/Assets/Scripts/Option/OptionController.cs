//using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Windows.Forms;
using Application = UnityEngine.Application;
using Screen = UnityEngine.Screen;

public class OptionController : MonoBehaviour
{
    private string dataPath;

    public GameOptionsData currentOption;
    public static Option defaultOption;
    private UnityEngine.Resolution[] resolutions;

    /// <summary>
    /// 게임 옵션 저장용 클래스
    /// </summary>
    [System.Serializable]

    public class GameOptionsData
    {
        public VideoData video;
        public AudioData audio;
        public LanguageData language;
        public ControlData control;
    }

    [System.Serializable]
    public class VideoData
    {
        public string resolution;
        public string quality;
        public string screenMode;

        public void SetDefault()
        {
            if(resolution == null) resolution = defaultOption.resolution;
            if(quality == null) quality = defaultOption.quality;
            if(screenMode == null) screenMode = defaultOption.screenMode;
        }
    }

    [System.Serializable]
    public class AudioData
    {
        public string masterVolume;
        public string bgmVolume;
        public string effectVolume;

        public void SetDefault()
        {
            if(masterVolume == null) masterVolume = defaultOption.masterVolume;
            if(bgmVolume == null) bgmVolume = defaultOption.bgmVolume;
            if(effectVolume == null) effectVolume = defaultOption.effectVolume;
        }
    }

    [System.Serializable]
    public class LanguageData
    {
        public string language;

        public void SetDefault()
        {
            if(language == null) language = defaultOption.language;
        }
    }

    [System.Serializable]
    public class ControlData
    {
        public string scrollSensitivity;
        public string moveForward;
        public string moveBackward;
        public string moveLeft;
        public string moveRight;
        public string sprint;
        public string interact;
        public string craft;
        public string ability;
        public string inventory;
        public string useHand;
        public string reload;
        public string useItem1;
        public string useItem2;
        public string useItem3;
        public string useItem4;
        
        public void SetDefault()
        {
            if(scrollSensitivity == null) scrollSensitivity = defaultOption.scrollSensitivity;
            if(moveForward == null) moveForward = defaultOption.moveForward;
            if(moveBackward == null) moveBackward = defaultOption.moveBackward;
            if(moveLeft == null) moveLeft = defaultOption.moveLeft;
            if(moveRight == null) moveRight = defaultOption.moveRight;
            if(sprint == null) sprint = defaultOption.sprint;
            if(interact == null) interact = defaultOption.interact;
            if(craft == null) craft = defaultOption.craft;
            if(ability == null) ability = defaultOption.ability;
            if(inventory == null) inventory = defaultOption.inventory;
            if(useHand == null) useHand = defaultOption.useHand;
            if(reload == null) reload = defaultOption.reload;
            if(useItem1 == null) useItem1 = defaultOption.useItem1;
            if(useItem2 == null) useItem2 = defaultOption.useItem2;
            if(useItem3 == null) useItem3 = defaultOption.useItem3;
            if(useItem4 == null) useItem4 = defaultOption.useItem4;
        }
    }

    /// <summary>
    /// Save / Load 함수
    /// </summary>

    public void SaveOption(GameOptionsData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, jsonData);
        LoadOption();
    }

    public void LoadOption()
    {
        if(File.Exists(dataPath))
        {
            try
            {
                string jsonData = File.ReadAllText(dataPath);
                currentOption = JsonUtility.FromJson<GameOptionsData>(jsonData);
                currentOption.video.SetDefault();
                currentOption.audio.SetDefault();
                currentOption.control.SetDefault();
                currentOption.language.SetDefault();
            } 
            catch
            {   
                string message = "Your option file is broken. It'll be reseted. Please run again.";
                string caption = "Fatal Error!";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result = MessageBox.Show(message, caption, button, icon);

                if (result == DialogResult.OK)
                {
                    File.Delete(dataPath);
                    Application.Quit();
                }
            }
        }
        else
        {
            currentOption = new GameOptionsData
            {
                video = new VideoData { resolution = "1920.1080", quality = "High", screenMode = "Fullscreen" },
                audio = new AudioData { masterVolume = "1", bgmVolume = "1", effectVolume = "1" },
                control = new ControlData
                {
                    scrollSensitivity = "1",
                    moveForward = "W",
                    moveBackward = "S",
                    moveLeft = "A",
                    moveRight = "D",
                    sprint = "LeftShift",
                    interact = "F",
                    craft = "C",
                    ability = "Q",
                    inventory = "E",
                    useHand = "LeftMouseClick",
                    reload = "R",
                    useItem1 = "1",
                    useItem2 = "2",
                    useItem3 = "3",
                    useItem4 = "4",
                },
                language = new LanguageData { language = "ENG" }
            };
            SaveOption(currentOption);   
        }
    }

    // SET VIDEO
    public void SetVideo()
    {
        // RESOLUTION + SCREENMODE
        bool targetMode = (currentOption.video.screenMode == "FullScreen") ? true : false;
        resolutions = Screen.resolutions;
        for(int i = 0; i < resolutions.Length; i++)
        {
            UnityEngine.Resolution resolution = resolutions[i];
            if(resolution.ToString().Contains(currentOption.video.resolution))
            {
                Screen.SetResolution(resolution.width, resolution.height, targetMode);
            }
        }
    }

    void Start()
    {
        dataPath = Application.persistentDataPath + "/options.json";
        LoadOption();
        SetVideo();
    }
}
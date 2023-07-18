using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Control : MonoBehaviour
{
    OptionController optionController;

    public GameObject VideoPanel;
    public GameObject AudioPanel;
    public GameObject ControlPanel;
    public GameObject LanguagePanel;

    // Video UI
    public TMP_Dropdown resolution;
    public TMP_Dropdown quality;
    public TMP_Dropdown screenMode;

    // Audio UI
    public Slider master;
    public Slider bgm;
    public Slider effect;

    // Control UI
    public Slider scroll;
    public TMP_Text moveForward;
    public TMP_Text moveBackward;
    public TMP_Text moveLeft;
    public TMP_Text moveRight;
    public TMP_Text sprint;
    public TMP_Text interact;
    public TMP_Text craft;
    public TMP_Text ability;
    public TMP_Text inventory;
    public TMP_Text useHand;
    public TMP_Text reload;
    public TMP_Text useItem1;
    public TMP_Text useItem2;
    public TMP_Text useItem3;
    public TMP_Text useItem4;

    // Language UI

    void Start()
    {
        optionController = GameObject.FindObjectOfType<OptionController>();
    }

    public void OpenVideo()
    {
        VideoPanel.SetActive(true);
        AudioPanel.SetActive(false);
        ControlPanel.SetActive(false);
        LanguagePanel.SetActive(false);
    }

    public void OpenAudio()
    {
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(true);
        ControlPanel.SetActive(false);
        LanguagePanel.SetActive(false);
    }

    public void OpenControl()
    {
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(false);
        ControlPanel.SetActive(true);
        LanguagePanel.SetActive(false);
    }

    public void OpenLanguage()
    {
        VideoPanel.SetActive(false);
        AudioPanel.SetActive(false);
        ControlPanel.SetActive(false);
        LanguagePanel.SetActive(true);
    }

    public void LoadVideo(OptionController.VideoData data)
    {

    }

    public void LoadAudio(OptionController.AudioData data)
    {
        master.value = float.Parse(data.masterVolume);
        bgm.value = float.Parse(data.bgmVolume);
        effect.value = float.Parse(data.effectVolume);
    }

    public void LoadControl(OptionController.ControlData data)
    {
        scroll.value = float.Parse(data.scrollSensitivity);
        moveForward.text = data.moveForward;
        moveBackward.text = data.moveBackward;
        moveLeft.text = data.moveLeft;
        moveRight.text = data.moveRight;
        sprint.text = data.sprint;
        craft.text = data.craft;
        interact.text = data.interact;
        ability.text = data.ability;
        inventory.text = data.inventory;
        useHand.text = data.useHand;
        reload.text = data.reload;
        useItem1.text = data.useItem1;
        useItem2.text = data.useItem2;
        useItem3.text = data.useItem3;
        useItem4.text = data.useItem4;
    }

    public void LoadLanguage(OptionController.LanguageData data)
    {

    }

    public void LoadData()
    {
        OptionController option = GameObject.FindObjectOfType<OptionController>();
        LoadVideo(option.currentOption.video);
        LoadAudio(option.currentOption.audio);
        LoadControl(option.currentOption.control);
        LoadLanguage(option.currentOption.language);
    }

    public void Save()
    {
        OptionController.GameOptionsData newOption = new OptionController.GameOptionsData {
            video = new OptionController.VideoData {
                resolution = resolution.options[resolution.value].text,
                quality = quality.options[quality.value].text,
                screenMode = screenMode.options[screenMode.value].text
            },
            audio = new OptionController.AudioData {
                masterVolume = master.value.ToString(),
                bgmVolume = bgm.value.ToString(),
                effectVolume = effect.value.ToString()
            },
            control = new OptionController.ControlData {
                scrollSensitivity = scroll.value.ToString(),
                moveForward = moveForward.text,
                moveBackward = moveBackward.text,
                moveLeft = moveLeft.text,
                moveRight = moveRight.text,
                sprint = sprint.text,
                interact = interact.text,
                craft = craft.text,
                ability = ability.text,
                inventory = inventory.text,
                useHand = useHand.text,
                reload = reload.text,
                useItem1 = useItem1.text,
                useItem2 = useItem2.text,
                useItem3 = useItem3.text,
                useItem4 = useItem4.text,
            },
            language = new OptionController.LanguageData {
                language = "ENG"
            }
        };

        optionController.SaveOption(newOption);
    }
}
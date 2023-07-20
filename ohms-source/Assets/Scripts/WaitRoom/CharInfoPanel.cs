using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharInfoPanel : MonoBehaviour
{
    public GameObject SkillIcon;
    public Slider StaminaSlider;
    public TMP_Text SlotKey1;
    public TMP_Text SlotKey2;
    public TMP_Text SlotKey3;
    public TMP_Text SlotKey4;

    OptionController optionController;

    void Start()
    {
        SkillIcon.SetActive(false);
        optionController = FindObjectOfType<OptionController>();
        SetSlotKeyText();
    }

    public void SetSlotKeyText()
    {
        OptionController.ControlData currentData = optionController.currentOption.control;
        SlotKey1.text = currentData.useItem1;
        SlotKey2.text = currentData.useItem2;
        SlotKey3.text = currentData.useItem3;
        SlotKey4.text = currentData.useItem4;
    }

    public void SetIcon(Sprite target)
    {
        SkillIcon.GetComponent<Image>().sprite = target;
        SkillIcon.SetActive(true);
    }
}
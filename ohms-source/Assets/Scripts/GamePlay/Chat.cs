using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chat : MonoBehaviour
{
    public GameObject chatText;
    public TMP_InputField chatInputField;

    public void WriteChat(string[] content)
    {
        GameObject ContentLayout = GameObject.Find("Canvas").transform.Find("ChatLog").transform.Find("Viewport").transform.Find("Content").gameObject;
        GameObject newText = Instantiate(chatText, ContentLayout.transform);
        newText.GetComponent<TMP_Text>().text = string.Format("[{0}] {1}", content[0], content[1]);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            PlayerChat(chatInputField.text);
            //chatInputField.ActivateInputField();
            chatInputField.Select();
            EventSystem.current.SetSelectedGameObject(null);

        }
    }

    public void PlayerChat(string input)
    {
        if(input != "")
        {
            string[] playerChat = new string[] { "gugyeoj1n", input };
            WriteChat(playerChat);
            chatInputField.text = "";
        }
    }
}
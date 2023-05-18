using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chat : MonoBehaviour
{
    public GameObject chatText;

    public void WriteChat(string[] content)
    {
        GameObject ContentLayout = GameObject.Find("Canvas").transform.Find("ChatLog").transform.Find("Viewport").transform.Find("Content").gameObject;
        GameObject newText = Instantiate(chatText, ContentLayout.transform);
        newText.GetComponent<TMP_Text>().text = string.Format("[{0}] {1}", content[0], content[1]);
    }
}
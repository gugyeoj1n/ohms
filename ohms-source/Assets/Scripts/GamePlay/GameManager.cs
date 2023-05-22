using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isStarted = false;
    public bool isPaused = false;
    private TimeManager timeManager;
    public TMP_Text countText;
    public GameObject player;
    Chat chat;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        chat = GetComponent<Chat>();
        StartCoroutine(gameStart());
    }

    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(1f);
        countText.text = "3";
        yield return new WaitForSeconds(1f);
        countText.text = "2";
        yield return new WaitForSeconds(1f);
        countText.text = "1";
        yield return new WaitForSeconds(1f);
        countText.text = "";
        isStarted = true;
        string[] startChat = new string[] { "System", "gugyeoj1n 님과 hyunseo24 님의 게임이 시작되었습니다!" };
        chat.WriteChat(startChat);
        //GetComponent<AudioSource>().Play();
        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<InventoryOpen>().enabled = true;
        player.GetComponent<Inventory>().enabled = true;
    }
}

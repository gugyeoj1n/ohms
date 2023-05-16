using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private TimeManager timeManager;
    public TMP_Text countText;
    public GameObject player;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
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
        timeManager.game = true;
        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<InventoryOpen>().enabled = true;
        player.GetComponent<Inventory>().enabled = true;
    }
}

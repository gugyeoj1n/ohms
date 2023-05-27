using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class RoomData : MonoBehaviourPunCallbacks
{
    public GameObject InfoPanel;
    TMP_Text roomNameText;
    TMP_Text roomHostText;
    TMP_Text winRateText;
    public GameObject NoRoomText;

    public string roomname;
    public string hostname;
    public string winrate;

    LobbyManager lobby;
    
    void Start()
    {
        lobby = GameObject.Find("LobbyManager").transform.gameObject.GetComponent<LobbyManager>();
        InfoPanel = GameObject.Find("Canvas").transform.Find("Base").transform.Find("Lobby").transform.Find("RoomInfoPanel").transform.GetChild(0).transform.gameObject;
        roomNameText = InfoPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        roomHostText = InfoPanel.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        winRateText = InfoPanel.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
    }

    public void OpenRoomInfo()
    {
        if(!lobby.roomDict.ContainsKey(roomname))
        {
            NoRoomText.SetActive(true);
            return;
        }
        NoRoomText.SetActive(false);
        InfoPanel.SetActive(true);
        roomNameText.text = roomname;
        roomHostText.text = hostname;
        winRateText.text = string.Format("Win Rate {0}%", winrate);
    }
}
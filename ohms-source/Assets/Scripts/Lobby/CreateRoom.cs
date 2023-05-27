using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateRoom : MonoBehaviour
{
    bool isCreateOpened = false;
    public GameObject CreatePanel;
    public TMP_InputField roomNameInput;

    void Start()
    {
        roomNameInput.text = string.Format("{0} 님의 게임", PhotonNetwork.NickName);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isCreateOpened) OpenCreatePanel();
        }
    }

    public void OpenCreatePanel()
    {
        isCreateOpened = !isCreateOpened;
        CreatePanel.SetActive(isCreateOpened);
    }

    public void CreateNewRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;
        string roomNameText = roomNameInput.text;
        Hashtable cp = new Hashtable() {
            { "hostName", PlayerInfo.PlayerName },
            { "winRate", PlayerInfo.WinRate.ToString() }
        };
        ro.CustomRoomProperties = cp;
        ro.CustomRoomPropertiesForLobby = new string[] { "hostName", "winRate" };
        PhotonNetwork.CreateRoom(roomNameText, ro);
        Debug.Log("CREATE NEW ROOM");
        SceneManager.LoadScene(3);
    }
}
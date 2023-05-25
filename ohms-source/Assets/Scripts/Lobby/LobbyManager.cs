using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    string gameVersion = "1.0.0";

    public Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    public GameObject RoomPrefab;
    public Transform scrollContent;
    public GameObject Lobby;
    public GameObject ConnectText;

    public GameObject NameText;
    public GameObject RateText;
    public GameObject MoneyText;


    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        NameText.GetComponent<TMP_Text>().text = PlayerInfo.PlayerName;
        RateText.GetComponent<TMP_Text>().text = string.Format("Win {0}%", PlayerInfo.WinRate.ToString());
        MoneyText.GetComponent<TMP_Text>().text = string.Format("${0}", PlayerInfo.Money.ToString());
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    

    public override void OnConnectedToMaster()
    {
        Debug.Log("MASTER SERVER CONNECTED");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Lobby.SetActive(true);
        ConnectText.SetActive(false);
        Debug.Log("LOBBY CONNECTED");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() {0}", cause);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;

        foreach(var room in roomList)
        {
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            else
            {
                if(roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(RoomPrefab, scrollContent);
                    roomDict.Add(room.Name, _room);
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
    }
}
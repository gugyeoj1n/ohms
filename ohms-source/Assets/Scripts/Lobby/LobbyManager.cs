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
    public GameObject CreatePanel;
    public TMP_InputField roomNameInput;
    public GameObject Lobby;
    public GameObject ConnectText;

    bool isCreateOpened = false;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
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
        PhotonNetwork.CreateRoom(roomNameText, ro);
        Debug.Log("CREATE NEW ROOM");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
    }
}
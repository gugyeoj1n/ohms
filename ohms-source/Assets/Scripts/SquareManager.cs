using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using MongoDB.Driver;

public class SquareManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string gameVersion = "1.0.0";

    ///////////////////////////////////////////////
    // UI BIND
    public GameObject TabPanel;
    public TMP_Text PlayerListCount;
    public GameObject ConnectText;
    ///////////////////////////////////////////////

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
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        ConnectText.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string msg)
    {
        Debug.LogFormat("Room Join Failed! {0}", returnCode);
        Debug.LogFormat("Message : {0}", msg);
    }

    public void ExitToTitle()
    {
        PhotonNetwork.Disconnect();
        //GetComponent<FadeAnim>().BackToTitle();
    }
}

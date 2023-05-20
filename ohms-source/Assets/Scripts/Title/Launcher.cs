using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Bson;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    TMP_InputField name;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        name = GameObject.Find("Canvas").transform.Find("NameInput").transform.GetComponent<TMP_InputField>();
    }

    public void ConnectServer()
    {
        Connect();
    }

    void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = name.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");
        Debug.LogFormat("NOW NICKNAME : {0}", PhotonNetwork.NickName);
    }

    public override void OnDisconnected(DisconnectCause cause){
        Debug.LogFormat("OnDisconnected(), {0}", cause);
    }
}
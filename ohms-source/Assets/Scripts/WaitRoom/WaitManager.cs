using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPref;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0f, 3f, 0f), Quaternion.identity, 0);
    }
}
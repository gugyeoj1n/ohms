using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject UIpref;

    void Start()
    {
        GameObject nickname = Instantiate(UIpref);
        nickname.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

}
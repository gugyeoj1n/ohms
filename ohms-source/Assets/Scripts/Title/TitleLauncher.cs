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

public class TitleLauncher : MonoBehaviourPunCallbacks
{
    TMP_InputField name;

    void Start()
    {
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
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
    bool menuOpened = false;
    GameObject Menu;

    void Start()
    {
        Menu = GameObject.Find("Canvas").transform.Find("Menu").transform.gameObject;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void SetMenuActive()
    {
        menuOpened = !menuOpened;
        Menu.SetActive(menuOpened);
    }
}
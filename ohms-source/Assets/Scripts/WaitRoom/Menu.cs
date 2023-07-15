using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviourPunCallbacks
{
    private bool isOpened = false;
    public GameObject MenuPanel;
    AudioSource audioManager;
    public Slider soundSlider;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioSource>();
        soundSlider.value = audioManager.volume;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!isOpened)
            {
                isOpened = true;
            }
            else
            {
                isOpened = false;
            }
            MenuPanel.SetActive(isOpened);
        }
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if(SceneManager.GetActiveScene().name == "WaitRoom")
        {
            SceneManager.LoadScene("Lobby");
            return;
        }
    }

    public void ControlVolume()
    {
        audioManager.volume = soundSlider.value;
    }

    public void CloseMenu()
    {
        isOpened = false;
        MenuPanel.SetActive(false);
    }
}
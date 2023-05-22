using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCMenu : MonoBehaviour
{
    GameObject Menu;
    AudioSource audioManager;
    public Slider soundSlider;
    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        Menu = GameObject.Find("Canvas").transform.Find("Menu").transform.gameObject;
        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioSource>();
        soundSlider.value = audioManager.volume;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!gameManager.isPaused)
            {
                gameManager.isPaused = true;
            }
            else
            {
                gameManager.isPaused = false;
            }
            Menu.SetActive(gameManager.isPaused);
        }
    }

    public void ControlVolume()
    {
        audioManager.volume = soundSlider.value;
    }

    public void ContinueGame()
    {
        Menu.SetActive(false);
    }
}
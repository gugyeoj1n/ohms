using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCMenu : MonoBehaviour
{
    public float maxVolume = 0.35f;
    GameObject Menu;
    AudioSource backgroundMusic;
    public Slider soundSlider;
    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        Menu = GameObject.Find("Canvas").transform.Find("Menu").transform.gameObject;
        backgroundMusic = GetComponent<AudioSource>();
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
        backgroundMusic.volume = maxVolume * soundSlider.value;
    }

    public void ContinueGame()
    {
        Menu.SetActive(false);
    }
}
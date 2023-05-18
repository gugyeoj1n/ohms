using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public float time = 300f;
    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.isStarted)
        {
            if(time >= 0f)
            {
                time -= Time.deltaTime;
                timeText.text = Mathf.Floor(time / 60) + ":" + Mathf.Floor(time % 60);
            }
        }
        
    }
}
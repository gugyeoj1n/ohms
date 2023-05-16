using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public bool game = false;
    public float time = 300f;

    void Update()
    {
        if(game)
        {
            if(time >= 0f)
            {
                time -= Time.deltaTime;
                timeText.text = Mathf.Floor(time / 60) + ":" + Mathf.Floor(time % 60);
            }
        }
        
    }
}
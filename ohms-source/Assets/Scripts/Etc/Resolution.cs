using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    CanvasScaler canvas;
    void Start()
    {
        canvas = GetComponent<CanvasScaler>();
        float fixedAspectRatio = 9f / 16f; 
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if (currentAspectRatio > fixedAspectRatio) canvas.matchWidthOrHeight = 1;       
        else if (currentAspectRatio < fixedAspectRatio) canvas.matchWidthOrHeight = 0;
    }
}

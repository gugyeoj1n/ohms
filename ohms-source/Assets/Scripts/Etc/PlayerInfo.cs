using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static string PlayerName;
    public static double WinRate;
    public static int Money;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
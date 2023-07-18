using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public GameObject audio;
    public GameObject info;
    public GameObject option;
    void Awake()
    {
        DontDestroyOnLoad(audio);
        DontDestroyOnLoad(info);
        DontDestroyOnLoad(option);
        SceneManager.LoadScene(1);
    }
}
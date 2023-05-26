using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public GameObject audio;
    public GameObject info;
    void Awake()
    {
        DontDestroyOnLoad(audio);
        DontDestroyOnLoad(info);
        SceneManager.LoadScene(1);
    }
}
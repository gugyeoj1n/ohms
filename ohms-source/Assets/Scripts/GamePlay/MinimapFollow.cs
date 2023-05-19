using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{   
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 pos = new Vector3(player.transform.position.x, 20, player.transform.position.z);
        transform.position = pos;
    }
}
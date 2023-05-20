using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenCamFollow : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z + 5);
        transform.position = pos;
    }
}

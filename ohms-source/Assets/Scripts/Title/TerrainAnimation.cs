using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAnimation : MonoBehaviour
{
    float endX = 60f;
    float x = -60f;
    float speed;

    void Start()
    {
        speed = Random.Range(0.001f, 0.002f);
    }

    void Update()
    {
        if(x >= endX) Destroy(this.gameObject);
        x += speed;
        Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = pos;
    }
}

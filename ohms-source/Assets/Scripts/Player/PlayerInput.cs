using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float move { get; private set; }
    public float rotate { get; private set; }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
    }
}
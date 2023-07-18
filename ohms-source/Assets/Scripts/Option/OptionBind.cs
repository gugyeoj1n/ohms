using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBind : MonoBehaviour
{
    OptionController option;

    void Start()
    {
        option = GameObject.FindObjectOfType<OptionController>();
    }

}
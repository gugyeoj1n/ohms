using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool invenOpen = false;
    public GameObject inventory;


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(invenOpen)invenOpen = false;
            else invenOpen = true;
            inventory.SetActive(invenOpen);
            Debug.Log(string.Format("현재 invenOpen : {0}", invenOpen));
        }
    }
}

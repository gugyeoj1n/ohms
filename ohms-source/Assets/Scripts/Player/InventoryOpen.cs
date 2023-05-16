using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpen : MonoBehaviour
{
    private bool invenOpen = false;
    public GameObject inventoryUI;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(invenOpen)invenOpen = false;
            else invenOpen = true;
            inventory.InvenUpdate();
            inventoryUI.SetActive(invenOpen);
            Debug.Log(string.Format("현재 invenOpen : {0}", invenOpen));
        }
    }
}

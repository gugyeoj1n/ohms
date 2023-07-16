using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public GameObject table;
    public GameObject menu;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(table.GetComponent<AttributeTable>().tableOpened)
            {
                table.GetComponent<AttributeTable>().CloseTable();
            } else
            {
                menu.GetComponent<Menu>().ManageMenu();
            }
        }
    }
}
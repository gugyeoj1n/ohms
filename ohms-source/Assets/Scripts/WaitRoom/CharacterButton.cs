using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public CharacterObject info;

    public void Select()
    {
        GameObject table = GameObject.Find("Attribute Table");
        if(table != null)
            table.SendMessage("LoadSelectedCharacter", info);
    }
}
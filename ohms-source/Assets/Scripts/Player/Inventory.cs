using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    Dictionary<string, int> inven = new Dictionary<string, int>();
    private int maxX = 7;
    private int maxY = 3;
    private string iconPath = "InventoryImages/";

    void Start()
    {
        inven.Add("gear", 5);
        inven.Add("circuit", 2);
    }

    public void InvenUpdate()
    {
        int x = 0;
        int y = 1;
        foreach(KeyValuePair<string, int> item in inven)
        {
            GameObject inv = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
            GameObject targetSlot = inv.transform.Find(string.Format("SlotLine{0}", y)).gameObject.transform.Find(string.Format("Slot ({0})", x)).gameObject;
            GameObject slotImage = targetSlot.transform.Find("Item").gameObject;
            GameObject slotText = targetSlot.transform.Find("ItemCountText").gameObject;
            Sprite icon = Resources.Load<Sprite>(iconPath + item.Key);
            slotImage.GetComponent<Image>().sprite = icon;
            Debug.Log(iconPath + item.Key);
            slotText.GetComponent<TMP_Text>().text = string.Format("{0}", item.Value);
            if(x >= 0 && x < maxX) x++;
        }
    }
}

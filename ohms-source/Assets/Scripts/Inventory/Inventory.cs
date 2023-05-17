using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inven = new Dictionary<string, int>();
    private int maxX = 5;
    private int maxY = 3;
    private string iconPath = "InventoryImages/";

    public void InvenUpdate()
    {
        int x = 0;
        int y = 1;
        foreach(KeyValuePair<string, int> item in inven)
        {
            if(x < maxX)
            {
                Debug.Log("1. x < maxX");
                PushItem(item.Key, item.Value, x, y);
                x++;
            }
            else
            {
                if(y < maxY)
                {
                    Debug.Log("2. y < maxY");
                    y++;
                    x = 0;
                    PushItem(item.Key, item.Value, x, y);
                    x = 1;
                }
                else
                {
                    Debug.Log("INVENTORY FULL");
                }
            }
        }
    }

    GameObject FindSlot(int x, int y)
    {
        GameObject inv = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
        GameObject targetSlot = inv.transform.Find(string.Format("SlotLine{0}", y)).gameObject.transform.Find(string.Format("Slot ({0})", x)).gameObject;
        return targetSlot;
    }

    public void PushItem(string itemName, int itemCount, int x, int y)
    {
        GameObject targetSlot = FindSlot(x, y);
        GameObject slotImage = targetSlot.transform.Find("Item").gameObject;
        GameObject slotText = targetSlot.transform.Find("ItemCountText").gameObject; 
        Sprite icon = Resources.Load<Sprite>(iconPath + itemName);
        slotImage.GetComponent<Image>().sprite = icon;
        slotImage.GetComponent<Image>().enabled = true;
        slotText.GetComponent<TMP_Text>().text = string.Format("{0}", itemCount);
        slotText.GetComponent<TMP_Text>().enabled = true;
    }

    void PopItem(int x, int y)
    {
        GameObject targetSlot = FindSlot(x, y);
        GameObject slotImage = targetSlot.transform.Find("Item").gameObject;
        GameObject slotText = targetSlot.transform.Find("ItemCountText").gameObject; 
        slotImage.GetComponent<Image>().enabled = false;
        slotText.GetComponent<TMP_Text>().enabled = false;
    }
}

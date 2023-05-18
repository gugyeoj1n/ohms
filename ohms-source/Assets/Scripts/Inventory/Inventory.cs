using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inven = new Dictionary<string, int>();
    public List<string> invenName = new List<string>();
    public List<int> invenCount = new List<int>();
    private int maxX = 5;
    private int maxY = 2;
    private string iconPath = "InventoryImages/";

    public void InvenUpdate()
    {
        for(int i = 0; i < maxX; i++)
        {
            for(int j = 0; j < maxY; j++)
            {
                GameObject targetSlot = FindSlot(i, j);
                GameObject slotImage = targetSlot.transform.Find("Item").gameObject;
                GameObject slotText = targetSlot.transform.Find("ItemCountText").gameObject;
                slotImage.GetComponent<Image>().enabled = false;
                slotText.GetComponent<TMP_Text>().enabled = false;
            }
        }

        int x = 0;
        int y = 0;
        for(int i = 0; i < invenName.Count; i++)
        {
            if(x < maxX)
            {
                PushItem(invenName[i], invenCount[i], x, y);
                x++;
            }
            else
            {
                if(y < maxY)
                {
                    y++;
                    x = 0;
                    PushItem(invenName[i], invenCount[i], x, y);
                    x = 1;
                }
            }
        }
    }

    GameObject FindSlot(int x, int y)
    {
        GameObject inv = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
        GameObject targetSlot = inv.transform.Find(string.Format("SlotLine{0}", y)).gameObject.transform.Find(string.Format("Slot{0}", x)).gameObject;
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

    public void PopItem(int x, int y)
    {
        int idx = x * 5 + y;
        Debug.Log(string.Format("{0} {1} {2}", x, y, idx));
        invenName.RemoveAt(idx);
        invenCount.RemoveAt(idx);
        InvenUpdate();
    }
}

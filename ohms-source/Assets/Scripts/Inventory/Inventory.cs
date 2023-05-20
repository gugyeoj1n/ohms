using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System;

public class Inventory : MonoBehaviour
{
    public class Item
    {
        public int Amount { get; set; }
        public string Name { get; set; }
        public bool Hand { get; set; }
    }

    private string[] itemArray = new string[] {
        "battery", "bolt", "bullet", "circuit", "cutter",
        "fabric", "gear", "lighter", "nail", "pipe",
        "revolver", "rope", "tape", "wire", "wrench",
        "ironbar", "battery2", "flashlight", "crowbar", "drug",
        "gunpowder", "radio", "burger", "water",
    };

    private string[] handItemArray = new string[] {
        "revolver", "flashlight", "crowbar", "fabric", "gear", "lighter", "nail", "pipe",
    };

    public List<Item> inven = new List<Item>();
    public string NameInHand;
    private int maxX = 6;
    private int maxY = 3;
    private string iconPath = "InventoryImages/";
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

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
        for(int i = 0; i < inven.Count; i++)
        {
            if(x < maxX)
            {
                PushItem(inven[i].Name, inven[i].Amount, x, y);
                x++;
            }
            else
            {
                if(y < maxY)
                {
                    y++;
                    x = 0;
                    PushItem(inven[i].Name, inven[i].Amount, x, y);
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
        if(x > maxX)
        {
            DropHandItem();
        } else {
            int idx = x * 6 + y;
            Debug.Log(string.Format("{0} {1} {2}", x, y, idx));
            inven.RemoveAt(idx);
        }

        InvenUpdate();
    }

    public void EquipItem(int x, int y)
    {
        int idx = x * 6 + y;
        if(inven[idx].Hand == true)
        {
            string targetName = inven[idx].Name;
            int targetCnt = inven[idx].Amount;
            GameObject inv = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
            GameObject toolSlotImg = inv.transform.Find("ToolSlot").gameObject.transform.Find("ToolItem").gameObject;
            GameObject toolSlotCnt = inv.transform.Find("ToolSlot").gameObject.transform.Find("ItemCountText").gameObject;
            Sprite icon = Resources.Load<Sprite>(iconPath + inven[idx].Name);
            if(NameInHand != "") {
                Debug.Log("ALREADY EXIST");
                inven[idx].Name = NameInHand;
                inven[idx].Amount = int.Parse(toolSlotCnt.GetComponent<TMP_Text>().text);
            } else PopItem(x, y);
            toolSlotImg.GetComponent<Image>().sprite = icon;
            toolSlotImg.GetComponent<Image>().enabled = true;
            toolSlotCnt.GetComponent<TMP_Text>().text = string.Format("{0}", targetCnt);
            toolSlotCnt.GetComponent<TMP_Text>().enabled = true;
            NameInHand = targetName;

            InvenUpdate();
        }
        else
        {
            gameManager.SendMessage("WriteChat", new string[] { "System", "해당 아이템은 장착할 수 없습니다."} );
        }
    }

    public void DropHandItem()
    {
        GameObject inv = GameObject.Find("Canvas").transform.Find("Inventory").gameObject;
        GameObject toolSlotImg = inv.transform.Find("ToolSlot").gameObject.transform.Find("ToolItem").gameObject;
        GameObject toolSlotCnt = inv.transform.Find("ToolSlot").gameObject.transform.Find("ItemCountText").gameObject;
        toolSlotImg.GetComponent<Image>().enabled = false;
        toolSlotCnt.GetComponent<TMP_Text>().enabled = false;
        NameInHand = "";
    }

    public void GetRandomItem(GameObject other)
    {
        for(int i = 0; i < Random.Range(1, 5); i++)
        {
            if(inven.Count == 18) {
                Debug.Log("INVENTORY FULL!!!");
                string[] fullInven = new string[] { "System", "인벤토리에 빈 공간이 없습니다." };
                gameManager.SendMessage("WriteChat", fullInven);
                return;
            }
            string itemName = itemArray[Random.Range(0, itemArray.Length)];
            string[] messages = new string[] { "System", itemName };
            gameManager.SendMessage("WriteChat", messages);
            int randCount = Random.Range(9, 14);
            bool canHand;
            if(Array.IndexOf(handItemArray, itemName) > -1) canHand = true;
            else canHand = false;

            for(int j = 0; j < inven.Count; j++)
            {
                if(itemName == inven[j].Name) // 이미 있는지 확인
                {
                    inven[j].Amount += randCount;
                    return;
                }
            }
            Item newItem = new Item();
            newItem.Amount = randCount;
            newItem.Name = itemName;
            newItem.Hand = canHand;
            inven.Add(newItem);
        }
    }
}

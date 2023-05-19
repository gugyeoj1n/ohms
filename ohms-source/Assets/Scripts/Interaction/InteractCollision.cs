using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollision : MonoBehaviour
{
    private bool opened = false;
    private GameObject player;
    private GameObject gameManager;
    private Outline outline;

    private string[] itemArray = new string[] {
        "battery", "bolt", "bullet", "circuit", "cutter",
        "fabric","gear", "lighter", "nail", "pipe",
        "revolver", "rope", "tape", "wire", "wrench"
    };

    void Start()
    {
        outline = GetComponent<Outline>();
        gameManager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            opened = true;
            Detect(opened);
            player = other.transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            opened = false;
            Detect(opened);
        }
    }

    void Update()
    {
        if(opened)
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("F ENTER");
                StartCoroutine(Interact(player));
                return;
            }
        }
    }

    void Detect(bool s)
    { 
        if(s)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    IEnumerator Interact(GameObject other)
    {
        other.GetComponent<PlayerMove>().Gather();
        yield return new WaitForSeconds(1.5f);
        GetRandomItem(player);
        player.GetComponent<Inventory>().InvenUpdate();
        Destroy(this.gameObject);
    }

    void GetRandomItem(GameObject other)
    {
        for(int i = 0; i < Random.Range(1, 5); i++)
        {
            List<string> items = other.GetComponent<Inventory>().invenName;
            List<int> itemCounts = other.GetComponent<Inventory>().invenCount;
            if(items.Count == 10) {
                Debug.Log("INVENTORY FULL!!!");
                string[] fullInven = new string[] { "System", "인벤토리에 빈 공간이 없습니다." };
                gameManager.SendMessage("WriteChat", fullInven);
                return;
            }
            string itemName = itemArray[Random.Range(0, 15)];
            string[] messages = new string[] { "System", itemName };
            gameManager.SendMessage("WriteChat", messages);
            int randCount = Random.Range(9, 14);
            if(items.Contains(itemName))
                other.GetComponent<Inventory>().invenCount[items.IndexOf(itemName)] += randCount;
            else
            {
                other.GetComponent<Inventory>().invenName.Add(itemName);
                other.GetComponent<Inventory>().invenCount.Add(randCount);
            }
        }
    }
}

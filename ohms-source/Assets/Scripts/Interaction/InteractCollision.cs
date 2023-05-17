using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollision : MonoBehaviour
{
    private bool opened = false;
    private GameObject player;
    private Outline outline;

    private string[] items = new string[] {
        "battery", "bolt", "bullet", "circuit", "cutter",
        "fabric","gear", "lighter", "nail", "pipe",
        "revolver", "rope", "tape", "wire", "wrench"
    };

    void Start()
    {
        outline = GetComponent<Outline>();
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
            outline.OutlineWidth = 2f;
        }
        else
        {
            outline.OutlineWidth = 0f;
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
            string itemName = items[Random.Range(0, 15)];
            int randCount = Random.Range(1, 4);
            Dictionary<string, int> itemDict = other.GetComponent<Inventory>().inven;
            if(itemDict.ContainsKey(itemName))
                itemDict[itemName] += randCount;
            else
                other.GetComponent<Inventory>().inven.Add(itemName, randCount);
        }
    }
}

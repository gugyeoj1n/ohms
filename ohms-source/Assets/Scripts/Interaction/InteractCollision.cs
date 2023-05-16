using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollision : MonoBehaviour
{
    private bool opened = false;
    private GameObject player;
    private Outline outline;

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
        Destroy(this.gameObject);
    }
}

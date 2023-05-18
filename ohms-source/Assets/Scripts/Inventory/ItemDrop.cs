using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 DefaultPos;
    private GraphicRaycaster gr;
    private Inventory inven;

    void Start()
    {
        gr = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        inven = GameObject.Find("Player").GetComponent<Inventory>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; 
        this.transform.position = currentPos;
        Color color = GetComponent<Image>().color;
        color.a = 0.5f;
        GetComponent<Image>().color = color;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = DefaultPos;

        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(eventData, results);
        if(results.Count <= 0)
        {
            string slot = transform.parent.gameObject.name;
            string slotline = transform.parent.gameObject.transform.parent.gameObject.name;
            Debug.Log(string.Format("ItemDrop 실행, 슬롯 위치 {0} {1}", slotline[8], slot[4]));
            inven.PopItem(slotline[8] - '0', slot[4] - '0');
            return;
        }

        Color color = GetComponent<Image>().color;
        color.a = 1f;
        GetComponent<Image>().color = color;
    }
}

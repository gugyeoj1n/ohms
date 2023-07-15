using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Nickname : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerName;
    PlayerManager target;

    [SerializeField]
    private Vector3 screenOffset = new Vector3(2f, 5f, 2f);
    Transform targetTransform;
    Vector3 targetPosition;

    public void SetTarget(PlayerManager _target)
    {
        if(_target == null) return;

        target = _target;
        playerName.text = target.photonView.Owner.NickName;

        targetTransform = target.transform;
    }

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        targetPosition = targetTransform.position;
        targetPosition.y += 2.5f;
        this.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
    }

    /*void LateUpdate()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        targetPosition = targetTransform.position;
        this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;    
    }*/
}
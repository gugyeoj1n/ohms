using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Doctor41 : MonoBehaviourPun
{
    private Animator playerAnim;
    [SerializeField]
    private bool isActivated = false;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!photonView.IsMine) return;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!isActivated)
            {
                isActivated = true;
                playerAnim.SetBool("SkillActivated", isActivated);
                Brutality();
            }
        }
    }

    void Brutality()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CaptainSqualo : MonoBehaviourPun
{
    private Animator playerAnim;
    [SerializeField]
    private bool isActivated = false;
    public GameObject Cannon;

    public float cooldown = 5f;

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
                StartCoroutine(Salpare());
            }
            else
            {
                Debug.Log("쿨타임임 ㅋ");
            }
        }
    }

    IEnumerator Salpare()
    {
        Vector3 cannonPosition = this.transform.position + transform.forward * 3f;
        PhotonNetwork.Instantiate("SkillEffect/" + Cannon.name, cannonPosition, transform.rotation);
        yield return new WaitForSeconds(cooldown);
        isActivated = false;
    }
}

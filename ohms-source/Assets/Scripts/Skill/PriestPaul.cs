using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PriestPaul : MonoBehaviourPun
{
    private Animator playerAnim;
    [SerializeField]
    private bool isActivated = false;
    public GameObject BuffParticle;

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
                StartCoroutine(Sanctuary());
            }
            else
            {
                Debug.Log("쿨타임임 ㅋ");
            }
        }
    }

    IEnumerator Sanctuary()
    {
        PhotonNetwork.Instantiate("SkillEffect/" + BuffParticle.name, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        isActivated = false;
    }
}

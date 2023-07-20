using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PriestPaul : MonoBehaviourPun
{
    private NavMeshAgent agent;
    private Animator playerAnim;
    [SerializeField]
    private bool isActivated = false;
    public GameObject BuffParticle;
    GameObject CooldownIcon;

    void Start()
    {
        CooldownIcon = GameObject.Find("SkillPanel").transform.GetChild(1).gameObject;
        playerAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if(!photonView.IsMine) return;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!isActivated)
            {
                agent.ResetPath();
                agent.isStopped = true;
                playerAnim.SetTrigger("SkillActivated");
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
        isActivated = true;
        PhotonNetwork.Instantiate("SkillEffect/" + BuffParticle.name, this.transform.position, Quaternion.identity);
        CooldownIcon.SetActive(true);
        Image fillImage = CooldownIcon.transform.GetChild(0).GetComponent<Image>();

        float cooltime = 5f;
        float startTime = Time.time;
        float startFill = 1f;
        float endFill = 0f;

        while(Time.time < startTime + cooltime)
        {
            float targetTime = Time.time - startTime;
            float currentFill = Mathf.Lerp(startFill, endFill, targetTime / cooltime);
            fillImage.fillAmount = currentFill;
            yield return null;
        }

        CooldownIcon.SetActive(false);
        fillImage.fillAmount = 1f;
        isActivated = false;
    }
}

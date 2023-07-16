using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owen : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField]
    private bool isActivated = false;
    [SerializeField]
    private int inputCount = 0;
    private int combo = 0;

    [SerializeField]
    private Collider triggered;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!isActivated)
            {
                isActivated = true;
                playerAnim.SetBool("SkillActivated", isActivated);
            }
            else
            {
                inputCount++;
                combo = inputCount;
                playerAnim.SetTrigger(string.Format("Combo{0}", combo));

                if(inputCount == 3)
                {
                    inputCount = 0;
                    isActivated = false;
                    playerAnim.SetBool("SkillActivated", isActivated);
                }
                combo = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        triggered = other;
    }

    void OnTriggerExit(Collider other)
    {
        triggered = null;
    }

    public void ToughGuy()
    {
        if(triggered != null)
        {
            if(triggered.tag == "Player")
            {
                Debug.Log("HIT PLAYER !");
            }
        }
    }
}
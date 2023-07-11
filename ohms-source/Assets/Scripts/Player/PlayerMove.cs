using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 2.5f;
    public float rotateSpeed = 180f;
    public bool sprinting = false;
    public float stamina = 1f;
    public float staminaUseSpeed = 0.005f;
    float maxStamina = 1f;
    float lerpSpeed = 10f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnim;

    public Slider StaminaBar;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!photonView.IsMine) return;
        
        Rotate();
        Move();
        playerAnim.SetFloat("Move", playerInput.move);
        if(Input.GetKey(KeyCode.LeftShift) && stamina > 0f)
        {
            sprinting = true;
            moveSpeed = 5f;
        }
        else
        {
            sprinting = false;
            moveSpeed = 2.5f;
        }
        playerAnim.SetBool("Sprint", sprinting);
        Sprint(sprinting);
    }

    void Sprint(bool s)
    {
        if(s)
        {
            stamina -= staminaUseSpeed;
        }
        else
        {
            if(stamina < maxStamina)
                stamina += staminaUseSpeed * 0.5f;
        }
        StaminaBar.value = Mathf.Lerp(StaminaBar.value, stamina, Time.deltaTime * lerpSpeed);
    }

    void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }

    public void Gather()
    {
        playerAnim.SetTrigger("Gather");
    }
}
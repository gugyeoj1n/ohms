using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.0f;
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
        Rotate();
        Move();
        playerAnim.SetFloat("Move", playerInput.move);
        if(Input.GetKey(KeyCode.LeftShift) && stamina > 0f)
        {
            sprinting = true;
            moveSpeed = 4f;
        }
        else
        {
            sprinting = false;
            moveSpeed = 1f;
        }
        Sprint(sprinting);
        playerAnim.SetBool("Sprint", sprinting);
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

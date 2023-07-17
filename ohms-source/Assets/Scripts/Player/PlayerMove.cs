using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 2.5f;
    public float rotateSpeed = 180f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnim;


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
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 2.5f;
    //public float rotateSpeed = 180f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnim;

    Vector3 forward, right;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        forward = Camera.main.transform.forward;                                                
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90f, 0)) * forward;
    }

    void FixedUpdate()
    {
        if(!photonView.IsMine) return;
        
        Move();
        playerAnim.SetFloat("Move", playerInput.move);
    }

    void Move()
    {
        //Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        //playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);

        //Vector3 velocity = new Vector3(playerInput.rotate, 0, playerInput.move).normalized;
        //playerRigidbody.velocity = velocity * moveSpeed;
        //transform.LookAt(transform.position + velocity);

        Vector3 moveDist = forward * moveSpeed * Time.deltaTime * playerInput.move + right * moveSpeed * Time.deltaTime * playerInput.rotate;
        Vector3 direction = Vector3.Normalize(moveDist);

        if(direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += moveDist;
        }
    }

    public void Gather()
    {
        playerAnim.SetTrigger("Gather");
    }

}
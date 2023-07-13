using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;

    private Camera camera;

    private bool isMove;
    private Vector3 destination;

    private Animator playerAnim;

    void Start()
    {
        camera = Camera.main;
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }

    private void Move()
    {
        if(isMove)
        {
            float dist = Vector3.Distance(destination, transform.position);
            if(dist <= 0.1f)
            {
                isMove = false;
                return;
            }
            if(dist > 1f) dist = 1f;
            playerAnim.SetFloat("Move", dist);
            var dir = destination - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * moveSpeed;
        }
    }
}

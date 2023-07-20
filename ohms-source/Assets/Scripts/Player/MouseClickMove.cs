using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class MouseClickMove : MonoBehaviourPun
{
    NavMeshAgent agent;
    Animator anim;
    float targetMoveValue = 0f;
    float moveSpeed = 5f;

    Transform spot;
    LineRenderer lineRenderer;
    Coroutine draw;

    void Start()
    {
        spot = GameObject.Find("Spot").transform.GetChild(0).transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.white;
        lineRenderer.enabled = false;
    }

    public void SetMoveStatus()
    {
        agent.isStopped = false;
    }

    void Update()
    {
        if(!photonView.IsMine) return;
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
                targetMoveValue = 2f;
                spot.gameObject.SetActive(true);
                spot.position = hit.point + Vector3.up * 0.1f;
                if(draw != null) StopCoroutine(draw);
                draw = StartCoroutine(DrawPath());
            }
        }
        else if (agent.remainingDistance < 0.1f)
        {
            targetMoveValue = 0f;
            spot.gameObject.SetActive(false);
            if(draw != null)
            {
                StopCoroutine(draw);
                lineRenderer.enabled = false;
            }
        }

        if(targetMoveValue == 0f)
        {
            float currentMoveValue = anim.GetFloat("Move");
            float newMoveValue = Mathf.Lerp(currentMoveValue, targetMoveValue, moveSpeed * Time.deltaTime);
            anim.SetFloat("Move", newMoveValue);
        }
        else
        {
            anim.SetFloat("Move", targetMoveValue);
        }
    }

    IEnumerator DrawPath()
    {
        lineRenderer.enabled = true;
        yield return null;
        while(true)
        {
            int cnt = agent.path.corners.Length;
            lineRenderer.positionCount = cnt;
            for(int i = 0; i < cnt; i++)
            {
                lineRenderer.SetPosition(i, agent.path.corners[i]);
            }
            yield return null;
        }
    }
}
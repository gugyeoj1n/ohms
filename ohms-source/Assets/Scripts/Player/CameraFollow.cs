using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CameraFollow : MonoBehaviourPun
{
    bool followStarted = false;
    CinemachineVirtualCamera followCam;

    float minValue = 3f;
    float maxValue = 8f;
    float currentValue = 5f;

    public void StartFollow()
    {
        if(this.photonView.IsMine)
        {
            Debug.Log(photonView.ViewID);
            followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = this.transform;
            followCam.LookAt = this.transform;
            followStarted = true;
        }
    }

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        currentValue -= scrollWheel;
        followCam.m_Lens.OrthographicSize = Mathf.Clamp(currentValue, minValue, maxValue);
    }
}
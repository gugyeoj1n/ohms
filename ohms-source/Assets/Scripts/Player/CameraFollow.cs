using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CameraFollow : MonoBehaviourPun
{
    void Start()
    {
        if(this.photonView.IsMine)
        {
            var followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = this.transform;
            followCam.LookAt = this.transform;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CameraFollow : MonoBehaviourPun
{
    public void StartFollow()
    {
        if(this.photonView.IsMine)
        {
            Debug.Log(photonView.ViewID);
            var followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = this.transform;
            followCam.LookAt = this.transform;
        }
    }
}
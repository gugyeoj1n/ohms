using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Cannon : MonoBehaviourPun
{
    public GameObject Explosion;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2.2f);
        Vector3 particlePos = this.transform.position + transform.forward * 1.26f + transform.up * 1.5f;
        PhotonNetwork.Instantiate("SkillEffect/" + Explosion.name, particlePos, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
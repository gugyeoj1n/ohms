using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;
using Photon.Realtime;

public class WaitManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPref;
    // key 는 ViewID, value 는 세팅
    public Dictionary<int, CharacterObject> Settings;

    void Awake()
    {
        Settings = new Dictionary<int, CharacterObject>();
    }

    public void AddPlayerSetting(int id, CharacterObject setting)
    {
        if(!Settings.ContainsKey(id))
        {
            Settings.Add(id, setting);
        }
    }

    public void WritePlayerSetting(int id, CharacterObject info)
    {
        Settings[id] = info;
        foreach(KeyValuePair<int, CharacterObject> pair in Settings)
            Debug.LogFormat("{0} {1}", pair.Key, pair.Value);

    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0f, 3f, 0f), Quaternion.identity, 0);
        player.GetComponent<CameraFollow>().StartFollow();
        AddPlayerSetting(player.GetComponent<PhotonView>().ViewID, new CharacterObject());
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            List<int> keys = new List<int>(Settings.Keys);
            List<CharacterObject> values = new List<CharacterObject>(Settings.Values);

            stream.SendNext(keys);
            stream.SendNext(values);
        }
        else
        {
            List<int> keys = (List<int>)stream.ReceiveNext();
            List<CharacterObject> values = (List<CharacterObject>)stream.ReceiveNext();

            Settings.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                Settings.Add(keys[i], values[i]);
            }
        }
    }
}
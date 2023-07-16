using System.Security.Cryptography;
using System.IO;
using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEditor;
using Photon.Realtime;
using TMPro;

public class WaitManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject PlayerCardContentField;
    public GameObject PlayerCard;
    GameObject[] currentCards;
    private string iconPath = "CharacterIcons/";
    public GameObject playerPref;
    GameObject currentPlayer;
    // key 는 ViewID, value 는 세팅
    public Dictionary<int, CharacterObject> Settings;

    public bool isReady = false;
    public Image readyButton;
    Color green = new Color(0.5f, 1f, 0.5f, 1f);
    Color red = new Color(1f, 0.5f, 0.5f, 1f);

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonPeer.RegisterType(typeof(DictionaryData), (byte)'D', SerializeDictionaryData, DeserializeDictionaryData);
        Settings = new Dictionary<int, CharacterObject>();
    }

    public void AddPlayerSetting(int id, CharacterObject setting)
    {
        if(!Settings.ContainsKey(id))
        {
            Settings.Add(id, setting);
            foreach(KeyValuePair<int, CharacterObject> pair in Settings)
                Debug.LogFormat("{0} {1}", pair.Key, pair.Value);
            photonView.RPC(nameof(SyncDictionary), RpcTarget.All, CreateDictionaryData());
        }
    }

    public void WritePlayerSetting(int id, CharacterObject info)
    {
        Settings[id] = info;
        // 캐릭터 재소환
        Destroy(currentPlayer);
        photonView.RPC(nameof(ClearPlayer), RpcTarget.Others, id);
        currentPlayer = PhotonNetwork.Instantiate(info.avatar.name, new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
        currentPlayer.GetComponent<CameraFollow>().StartFollow();
        foreach(KeyValuePair<int, CharacterObject> pair in Settings)
            Debug.LogFormat("{0} {1}", pair.Key, pair.Value);
        photonView.RPC(nameof(SyncDictionary), RpcTarget.All, CreateDictionaryData());
    }

    [PunRPC]
    void ClearPlayer(int id)
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();

        foreach (PhotonView photonView in photonViews)
        {
            if (photonView.ViewID == id)
            {
                GameObject targetObject = photonView.gameObject;
                Destroy(targetObject);
                return;
            }
        }
    }

    public void ManageCards()
    {
        Player[] currentList = PhotonNetwork.PlayerList;
        currentCards = new GameObject[currentList.Length];
        // 현존하는 카드 삭제 후 Instantiate
        Transform[] preList = PlayerCardContentField.GetComponentsInChildren<Transform>();
        if(preList != null)
        {
            for(int i = 1; i < preList.Length; i++)
                Destroy(preList[i].gameObject);
        }

        for(int i = 0; i < currentList.Length; i++)
        {
            GameObject NewPlayerCard = Instantiate(PlayerCard, PlayerCardContentField.transform);
            currentCards[i] = NewPlayerCard;
            NewPlayerCard.transform.GetChild(0).GetComponent<TMP_Text>().text = currentList[i].NickName;
        }
    }

    /*public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        int id = newPlayer.GetPhotonView().ViewID;
        AddPlayerSetting(id, new CharacterObject());
        CreatePlayerCard(id);
    }*/

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC(nameof(SyncDictionary), RpcTarget.All, CreateDictionaryData());
        ManageCards();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ManageCards();
    }

    public override void OnJoinedRoom()
    {
        currentPlayer = PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
        currentPlayer.GetComponent<CameraFollow>().StartFollow();
        ManageCards();
        //AddPlayerSetting(player.GetComponent<PhotonView>().ViewID, new CharacterObject());
    }

    // 딕셔너리 동기화

    [PunRPC]
    private void SyncDictionary(DictionaryData dictionaryData, PhotonMessageInfo info)
{
    Dictionary<int, CharacterObject> dictionary = DictionaryFromDictionaryData(dictionaryData);
    Settings = dictionary;
}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            byte[] dictionaryBytes = DictionaryToBytes(Settings);
            stream.SendNext(dictionaryBytes);
        }
        else
        {
            byte[] dictionaryBytes = (byte[])stream.ReceiveNext();
            Dictionary<int, CharacterObject> dictionary = DictionaryFromBytes(dictionaryBytes);
            Settings = dictionary;
        }
    }

    private Dictionary<int, CharacterObject> DictionaryFromDictionaryData(DictionaryData dictionaryData)
    {
        Dictionary<int, CharacterObject> dictionary = new Dictionary<int, CharacterObject>();

        for (int i = 0; i < dictionaryData.keys.Count; i++)
        {
            int key = dictionaryData.keys[i];
            byte[] valueBytes = dictionaryData.valuesBytes[i];
            if (valueBytes != null)
            {
                CharacterObject value = DeserializeCharacterObject(valueBytes);
                dictionary[key] = value;
            }
        }

        return dictionary;
    }

    private byte[] DictionaryToBytes(Dictionary<int, CharacterObject> dictionary)
    {
        List<int> keys = new List<int>(dictionary.Keys);
        List<byte[]> valuesBytes = new List<byte[]>();

        foreach (int key in keys)
        {
            CharacterObject value = dictionary[key];
            byte[] valueBytes = SerializeCharacterObject(value);
            valuesBytes.Add(valueBytes);
        }

        DictionaryData dictionaryData = new DictionaryData(keys, valuesBytes);
        return SerializeDictionaryData(dictionaryData);
    }

    private Dictionary<int, CharacterObject> DictionaryFromBytes(byte[] dictionaryBytes)
    {
        object obj = DeserializeDictionaryData(dictionaryBytes);
        DictionaryData dictionaryData = (DictionaryData)obj;
        List<int> keys = dictionaryData.keys;
        List<byte[]> valuesBytes = dictionaryData.valuesBytes;

        Dictionary<int, CharacterObject> dictionary = new Dictionary<int, CharacterObject>();

        for (int i = 0; i < keys.Count; i++)
        {
            int key = keys[i];
            byte[] valueBytes = valuesBytes[i];
            CharacterObject value = DeserializeCharacterObject(valueBytes);
            dictionary[key] = value;
        }

        return dictionary;
    }

    private byte[] SerializeCharacterObject(CharacterObject characterObject)
    {
        string json = JsonUtility.ToJson(characterObject);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    private CharacterObject DeserializeCharacterObject(byte[] valueBytes)
    {
        string json = System.Text.Encoding.UTF8.GetString(valueBytes);
        CharacterObject newObj = ScriptableObject.CreateInstance<CharacterObject>();
        JsonUtility.FromJsonOverwrite(json, newObj);
        return newObj;
    }

    private byte[] SerializeDictionaryData(object obj)
    {
        DictionaryData dicData = (DictionaryData) obj;
        string json = JsonUtility.ToJson(dicData);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    private object DeserializeDictionaryData(byte[] dictionaryBytes)
    {
        string json = System.Text.Encoding.UTF8.GetString(dictionaryBytes);
        return JsonUtility.FromJson<DictionaryData>(json);
    }

    private class DictionaryData
    {
        public List<int> keys;
        public List<byte[]> valuesBytes;

        public DictionaryData(List<int> keys, List<byte[]> valuesBytes)
        {
            this.keys = keys;
            this.valuesBytes = valuesBytes;
        }
    }

    private DictionaryData CreateDictionaryData()
    {
        List<int> keys = new List<int>(Settings.Keys);
        List<byte[]> valuesBytes = new List<byte[]>();
        foreach (var value in Settings.Values)
        {
            byte[] valueBytes = SerializeCharacterObject(value);
            valuesBytes.Add(valueBytes);
        }
        DictionaryData dictionaryData = new DictionaryData(keys, valuesBytes);
        return dictionaryData;
    }

    public void Ready()
    {
        isReady = !isReady;
        readyButton.color = (isReady) ? green : red;
    }
}
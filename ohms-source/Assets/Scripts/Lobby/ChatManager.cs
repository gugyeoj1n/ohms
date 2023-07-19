using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using AuthenticationValues = Photon.Chat.AuthenticationValues;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    public TMP_InputField chatInput;
    public GameObject chatPrefab;
    string username;
    public GameObject ContentLayout;

    void Start()
    {
        username = PhotonNetwork.NickName;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
            "1.0", new AuthenticationValues(username));
    }

    void Update()
    {
        if(chatClient != null)
            chatClient.Service();

        if(Input.GetKeyDown(KeyCode.Return))
        {
            ChatPublish(chatInput.text);
            chatInput.Select();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void DebugReturn(DebugLevel level, string mes)
    {

    }

    public void OnConnected()
    {
        Debug.Log("CHAT SERVER CONNECT!");
        chatClient.Subscribe(new string[] { "lobby" });
        chatClient.PublishMessage("lobby", string.Format("[System] {0} has joined the game.", username));
    }

    public void OnDisconnected()
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for(int i = 0; i < messages.Length; i++)
        {
            GameObject newText = Instantiate(chatPrefab, ContentLayout.transform);
            newText.GetComponent<TMP_Text>().text = string.Format("{0}", messages[i]);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void ChatPublish(string input)
    {
        if(input != "")
        {
            chatClient.PublishMessage("lobby", string.Format("[{0}] {1}", username, input));
            chatInput.text = "";
        }
    }
}
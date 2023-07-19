using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Bson;

public class TitleLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField id_input;
    public TMP_InputField pw_input;
    public TMP_Text loginStatusText;
    public TMP_Text errorText;
    public GameObject loginButtonText;
    public Button loginButton;
    public TMP_Text statusText;

    PlayerInfo playerInfo;
    
    bool isLogined = false;
    bool menuOpened = false;
    bool loginOpened = false;
    public GameObject Menu;
    public GameObject Login;
    public Control control;

    private string MONGO_URI;
    private string DB_NAME = "users";
    private MongoClient mongoClient;
    private IMongoDatabase db;

    void Start()
    {
        GetComponent<FadeAnim>().StartFadeIn();

        //TextAsset textFile = Resources.Load<TextAsset>("Database/link");
        //MONGO_URI = textFile.text;
        MONGO_URI = "mongodb+srv://gugyeoj1n:woojin9821@ohms-db.6nxwi80.mongodb.net/?retryWrites=true&w=majority";
        mongoClient = new MongoClient(MONGO_URI);
        db = mongoClient.GetDatabase(DB_NAME);

        if(PlayerInfo.PlayerName != "")
        {
            isLogined = true;
            loginStatusText.text = string.Format("Status : <#67FF8C>{0}", PlayerInfo.PlayerName);
            loginButtonText.GetComponent<TMP_Text>().text = "Log Out";
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuOpened) SetMenuActive();
            if(loginOpened) SetLoginActive();
        }
        if(id_input.isFocused == true)
        {
            if(Input.GetKeyDown(KeyCode.Tab)) pw_input.Select();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        if(PhotonNetwork.NickName == "")
        {
            statusText.text = "Log In First!";
            return;
        }
        GetComponent<FadeAnim>().TitleFadeOut();
    }

    public void SetMenuActive()
    {
        menuOpened = !menuOpened;
        if(menuOpened) control.LoadData();
        else control.Save();
        Menu.SetActive(menuOpened);
    }

    public void SetLoginActive()
    {
        loginOpened = !loginOpened;
        Login.SetActive(loginOpened);
    }

    public void TryLogin()
    {
        if(!isLogined)
        {
            if(id_input.text == "" || pw_input.text == "")
            {
                errorText.text = "Invalid input.";
                return;
            }
            var users = db.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("name", id_input.text);

            try {
                var checkUser = users.Find(filter).First();
                if(checkUser.GetValue("password") == pw_input.text)
                {
                    PhotonNetwork.NickName = id_input.text;
                    PlayerInfo.PlayerName = id_input.text;
                    PlayerInfo.WinRate = checkUser.GetValue("winRate").AsDouble;
                    PlayerInfo.Money = checkUser.GetValue("money").AsInt32;
                    loginStatusText.text = string.Format("Status : <#67FF8C>{0}", id_input.text);
                    id_input.text = "";
                    pw_input.text = "";
                    errorText.text = "";
                    isLogined = true;
                    statusText.text = "";
                    loginButtonText.GetComponent<TMP_Text>().text = "Logout";
                } else
                {
                    errorText.text = "Password doesn't match.";
                    return;
                }
            } catch {
                errorText.text = "ID doesn't exist.";
                return;
            } 
        } else
        {
            PhotonNetwork.NickName = "";
            loginStatusText.text = string.Format("Status : <#FF5D5D>Not Logged In");
            id_input.text = "";
            pw_input.text = "";
            errorText.text = "";
            loginButtonText.GetComponent<TMP_Text>().text = "Log In";
            isLogined = false;
        }
    }
    
    public void TryRegister()
    {
        if(id_input.text == "" || pw_input.text == "")
        {
            errorText.text = "Invalid input.";
            return;
        }

        var users = db.GetCollection<BsonDocument>("user");
        var filter = Builders<BsonDocument>.Filter.Eq("name", id_input.text);
        try
        {
            var checkUser = users.Find(filter).First();
            errorText.text = "ID already exists.";
            return;
        } catch 
        {
            errorText.text = "<#67FF8C>Account successfully created!";
            users.InsertOne(new BsonDocument
            {
                { "name", id_input.text },
                { "password", pw_input.text },
                { "winRate", 0.0 },
                { "money", 100 },
            });
        } 
    }
}
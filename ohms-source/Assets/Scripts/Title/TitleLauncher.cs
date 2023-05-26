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
    TMP_InputField name;
    public TMP_InputField id_input;
    public TMP_InputField pw_input;
    public TMP_Text loginStatusText;
    public TMP_Text errorText;
    public GameObject loginButtonText;
    public Button loginButton;
    public TMP_Text statusText;
    AudioSource audio;
    public Slider soundSlider;

    PlayerInfo playerInfo;
    
    bool isLogined = false;
    bool menuOpened = false;
    bool loginOpened = false;
    GameObject Menu;
    GameObject Login;

    private string MONGO_URI;
    private string DB_NAME = "users";
    private MongoClient mongoClient;
    private IMongoDatabase db;

    void Start()
    {
        audio = GameObject.Find("AudioManager").gameObject.GetComponent<AudioSource>();
        soundSlider.value = audio.volume;
        //TextAsset textFile = Resources.Load<TextAsset>("Database/link");
        //MONGO_URI = textFile.text;
        MONGO_URI = "mongodb+srv://gugyeoj1n:woojin9821@ohms-db.6nxwi80.mongodb.net/?retryWrites=true&w=majority";
        mongoClient = new MongoClient(MONGO_URI);
        db = mongoClient.GetDatabase(DB_NAME);
        Menu = GameObject.Find("Canvas").transform.Find("Menu").transform.gameObject;
        Login = GameObject.Find("Canvas").transform.Find("LoginBackground").transform.gameObject;

        if(PlayerInfo.PlayerName != "")
        {
            isLogined = true;
            loginStatusText.text = string.Format("로그인 상태 : <#67FF8C>{0}", PlayerInfo.PlayerName);
            loginButtonText.GetComponent<TMP_Text>().text = "Logout";
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

    public void ControlVolume()
    {
        audio.volume = soundSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        if(PhotonNetwork.NickName == "")
        {
            statusText.text = "로그인해 주세요!";
            return;
        }
        SceneManager.LoadScene(2);
    }

    public void SetMenuActive()
    {
        menuOpened = !menuOpened;
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
                errorText.text = "입력이 올바르지 않습니다.";
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
                    loginStatusText.text = string.Format("로그인 상태 : <#67FF8C>{0}", id_input.text);
                    id_input.text = "";
                    pw_input.text = "";
                    errorText.text = "";
                    isLogined = true;
                    statusText.text = "";
                    loginButtonText.GetComponent<TMP_Text>().text = "Logout";
                } else
                {
                    errorText.text = "비밀번호가 일치하지 않습니다.";
                    return;
                }
            } catch {
                errorText.text = "ID가 존재하지 않습니다.";
                return;
            } 
        } else
        {
            PhotonNetwork.NickName = "";
            loginStatusText.text = string.Format("로그인 상태 : <#FF5D5D>로그인되지 않음");
            id_input.text = "";
            pw_input.text = "";
            errorText.text = "";
            loginButtonText.GetComponent<TMP_Text>().text = "Login";
            isLogined = false;
        }
    }
    
    public void TryRegister()
    {
        if(id_input.text == "" || pw_input.text == "")
        {
            errorText.text = "입력이 올바르지 않습니다.";
            return;
        }

        var users = db.GetCollection<BsonDocument>("user");
        var filter = Builders<BsonDocument>.Filter.Eq("name", id_input.text);
        try
        {
            var checkUser = users.Find(filter).First();
            errorText.text = "ID가 이미 존재합니다.";
            return;
        } catch 
        {
            errorText.text = "<#67FF8C>계정이 생성되었습니다!";
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
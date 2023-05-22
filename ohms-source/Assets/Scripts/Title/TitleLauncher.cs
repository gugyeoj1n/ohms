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
        TextAsset textFile = Resources.Load<TextAsset>("Database/link");
        MONGO_URI = textFile.text;
        mongoClient = new MongoClient(MONGO_URI);
        db = mongoClient.GetDatabase(DB_NAME);
        Menu = GameObject.Find("Canvas").transform.Find("Menu").transform.gameObject;
        Login = GameObject.Find("Canvas").transform.Find("LoginBackground").transform.gameObject;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuOpened) SetMenuActive();
            if(loginOpened) SetLoginActive();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
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
                    loginStatusText.text = string.Format("로그인 상태 : <#67FF8C>{0}", id_input.text);
                    id_input.text = "";
                    pw_input.text = "";
                    errorText.text = "";
                    isLogined = true;
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
                {"name", id_input.text},
                {"password", pw_input.text},
                {"winRate", 0.0}
            });
        } 
    }
}
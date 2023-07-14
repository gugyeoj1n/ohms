using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeTable : MonoBehaviourPun
{
    private bool opened = false;
    private GameObject player;
    private Outline outline;

    private WaitManager waitManager;
    private CharacterObject selectedCharInfo;
    public CharacterObject[] CharacterData;

    public GameObject AttributePanel;
    public GameObject ContentField;
    public GameObject CharacterButton;

    public TMP_Text pointText;
    public TMP_Text stepText;
    public TMP_Text gatherText;
    public TMP_Text craftText;
    public TMP_Text resistText;
    public Image SelectedIcon;
    public TMP_Text description;

    int remainPoint = 3;
    int step = 0;
    int gather = 0;
    int craft = 0;
    int resist = 0;

    private string iconPath = "CharacterIcons/";

    void Start()
    {
        waitManager = GameObject.Find("WaitManager").gameObject.GetComponent<WaitManager>();
        outline = GetComponent<Outline>();
        LoadCharacterList();
    }

    void LoadCharacterList()
    {
        for(int i = 0; i < CharacterData.Length; i++)
        {
            GameObject charButton = Instantiate(CharacterButton, ContentField.transform);
            charButton.GetComponent<CharacterButton>().info = CharacterData[i];
            Sprite icon = Resources.Load<Sprite>(iconPath + CharacterData[i].name);
            Image target = charButton.transform.GetChild(0).GetComponent<Image>();
            target.sprite = icon;
        }
    }

    public void LoadSelectedCharacter(CharacterObject info)
    {
        remainPoint = 3;
        step = 0;
        gather = 0;
        craft = 0;
        resist = 0;
        pointText.text = "3 Points";
        stepText.text = info.step.ToString();
        gatherText.text = info.gather.ToString();
        craftText.text = info.craft.ToString();
        resistText.text = info.resist.ToString();
        description.text = info.description;
        selectedCharInfo = info;
        Sprite icon = Resources.Load<Sprite>(iconPath + info.name);
        SelectedIcon.sprite = icon;
    }

    public void Apply()
    {
        waitManager.WritePlayerSetting(player.GetComponent<PhotonView>().ViewID, selectedCharInfo);
    }

    /// <summary>
    /// 상호작용 가능한 오브젝트가 플레이어의 콜라이더를 감지할 때 자식 오브젝트로 있는 Detector를 감지하므로
    /// Collider other 의 부모 오브젝트가 플레이어가 된다
    /// </summary>

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponentInParent<PhotonView>().IsMine)
        {
            opened = true;
            Detect(opened);
            player = other.transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && other.GetComponentInParent<PhotonView>().IsMine)
        {
            opened = false;
            Detect(opened);
        }
    }

    void Update()
    {
        if(opened)
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("F ENTER");
                AttributePanel.SetActive(true);
                return;
            }
        }
    }

    public void CloseTable()
    {
        AttributePanel.SetActive(false);
    }

    bool PointAvailable()
    {
        return (remainPoint > 0) ? true : false;
    }

    public void StepPlus()
    {
        if(selectedCharInfo == null) return;

        if(PointAvailable())
        {
            remainPoint--;
            step++;
            RefreshStat();
        }
    }

    public void GatherPlus()
    {
        if(selectedCharInfo == null) return;

        if(PointAvailable())
        {
            remainPoint--;
            gather++;
            RefreshStat();
        }
    }
    
    public void craftPlus()
    {
        if(selectedCharInfo == null) return;

        if(PointAvailable())
        {
            remainPoint--;
            craft++;
            RefreshStat();
        }
    }

    public void ResistPlus()
    {
        if(selectedCharInfo == null) return;

        if(PointAvailable())
        {
            remainPoint--;
            resist++;
            RefreshStat();
        }
    }

    public void StepMinus()
    {
        if(selectedCharInfo == null) return;
        if(step > 0)
        {
            remainPoint++;
            step--;
            RefreshStat();
        }
    }

    public void GatherMinus()
    {
        if(selectedCharInfo == null) return;
        if(gather > 0)
        {
            remainPoint++;
            gather--;
            RefreshStat();
        }
    }

    public void craftMinus()
    {
        if(selectedCharInfo == null) return;
        if(craft > 0)
        {
            remainPoint++;
            craft--;
            RefreshStat();
        }
    }

    public void ResistMinus()
    {
        if(selectedCharInfo == null) return;
        if(resist > 0)
        {
            remainPoint++;
            resist--;
            RefreshStat();
        }
    }

    void RefreshStat()
    {
        pointText.text = string.Format("{0} Points", remainPoint);
        stepText.text = (selectedCharInfo.step + step).ToString();
        gatherText.text = (selectedCharInfo.gather + gather).ToString();
        craftText.text = (selectedCharInfo.craft + craft).ToString();
        resistText.text = (selectedCharInfo.resist + resist).ToString();
    }

    void Detect(bool s)
    { 
        if(s)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}

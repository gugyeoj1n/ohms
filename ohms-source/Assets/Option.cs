using UnityEngine;

[CreateAssetMenu(fileName = "Option", menuName = "Scriptable Object/Option", order = 0)]
public class Option : ScriptableObject {
    public string resolution;
    public string quality;
    public string screenMode;
    public string masterVolume;
    public string bgmVolume;
    public string effectVolume;
    public string language;
    public string scrollSensitivity;
    public string moveForward;
    public string moveBackward;
    public string moveLeft;
    public string moveRight;
    public string sprint;
    public string interact;
    public string craft;
    public string ability;
    public string inventory;
    public string useHand;
    public string reload;
    public string useItem1;
    public string useItem2;
    public string useItem3;
    public string useItem4;
}
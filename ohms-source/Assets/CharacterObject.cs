using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Object/Character", order = int.MaxValue)]
public class CharacterObject : ScriptableObject
{
    public string name;
    public int step;
    public int gather;
    public int craft;
    public int resist;
    public string ability;
    public string description;
    public GameObject avatar;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills
{
    public string skillName; //Name of the skill
    public string skillType; //Uses Attack or Special Attack stat
    public string skillElement; //Element type
    public int skillPower; //Power stat of skill
    public int skillMaxSP; //Max SP of skill
    public int skillCurrentSP; //Current SP of the Skill

    //Constructor
    public Skills(string name, string type, string element, int power, int maxSP)
    {
        this.skillName = name;
        this.skillType = type;
        this.skillElement = element;
        this.skillPower = power;
        this.skillMaxSP = maxSP;
    }
}

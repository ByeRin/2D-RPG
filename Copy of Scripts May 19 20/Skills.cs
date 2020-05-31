using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    protected string _skillName; //Name of the skill
    protected string _skillType; //Uses Attack or Special Attack stat
    protected string _skillElement; //Element type
    protected int _skillPower; //Power stat of skill
    protected int _skillMaxSP; //Max SP of skill
    protected int _skillCurrentSP; //Current SP of the Skill

    //Constructor
    protected Skills(string name, string type, string element, int power, int maxSP)
    {
        this._skillName = name;
        this._skillType = type;
        this._skillElement = element;
        this._skillPower = power;
        this._skillMaxSP = maxSP;
    }
}

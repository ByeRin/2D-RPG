using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class SkillsSO : ScriptableObject
{
    public string skillName; //Name of the skill
    public AttackPowerType skillAttackType;
    public ElementTypes skillElement; //Element type
    public int skillPower; //Power stat of skill
    public int skillMaxSP; //Max SP of skill
    public int skillCurrentSP; //Current SP of the Skill

    public enum ElementTypes
    {
        NonElemental,
        Spirit,
        Fire,
        Ice,
        Earth,
        Lightning,
        Water,
        Light,
        Dark,
        Robot,
        Rock,
        Poison,
        Air
    };

    public enum AttackPowerType
    {
        Attack,
        SpecialAttack
    };    
}

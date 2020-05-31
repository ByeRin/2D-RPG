using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit")]
public class UnitStatsScriptableObject : ScriptableObject
{
    [Header("Unit Info")]
    public string unitName;
    public ElementTypes elementType;
    public Sprite unitSprite;

    #region Base Stats
    protected float baseAttack = 10;
    protected float baseDefense = 10;
    protected float baseSpecialAttack = 10;
    protected float baseSpecialDefense = 10;
    protected float baseSpeed = 10;
    protected float baseMaxHP = 15;
    #endregion

    [Header("Level Up Multipliers")]
    #region Stat Level Up Multipliers
    public float attackMultiplier = 1;
    public float defenseMultiplier = 1;
    public float specialAttackMultiplier = 1;
    public float specialDefenseMultiplier = 1;
    public float speedMultiplier = 1;
    public float maxHPMultiplier = 3;
    #endregion

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

    [Header("Skills")]
    public List<SkillsSO> allSkills = new List<SkillsSO>();
    [Header("Level Required to Learn Skills")]
    public List<int> levelToLearnSkills = new List<int>();

    //CALCULATE STATS
    public (int attack, int defense, int specialAttack, int specialDefense, int speed, int maxHP)
        CalculateStats(int currentLevel)
    {
        int attack = Mathf.FloorToInt(baseAttack + (currentLevel * attackMultiplier));
        int defense = Mathf.FloorToInt(baseDefense + (currentLevel * defenseMultiplier));
        int specialAttack = Mathf.FloorToInt(baseSpecialAttack + (currentLevel * specialAttackMultiplier));
        int specialDefense = Mathf.FloorToInt(baseSpecialDefense + (currentLevel * specialDefenseMultiplier));
        int speed = Mathf.FloorToInt(baseSpeed + (currentLevel * speedMultiplier));
        int maxHP = Mathf.FloorToInt(baseMaxHP + (currentLevel * maxHPMultiplier));
        return (attack, defense, specialAttack, specialDefense, speed, maxHP);
    }
}

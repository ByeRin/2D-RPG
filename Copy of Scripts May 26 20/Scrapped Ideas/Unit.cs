using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit : MonoBehaviour
{
    public string unitName;
    public ElementTypes elementType;
    public int currentLevel = 1;
    public int currentXP = 0;
    public int nextLevelXPNeeded = 50;

    #region Base Stats
    protected float baseAttack = 10;
    protected float baseDefense = 10;
    protected float baseSpecialAttack = 10;
    protected float baseSpecialDefense = 10;
    protected float baseSpeed = 10;
    protected float baseMaxHP = 50;
    #endregion

    #region Stat Level Up Multipliers
    protected float attackMultiplier;
    protected float defenseMultiplier;
    protected float specialAttackMultiplier;
    protected float specialDefenseMultiplier;
    protected float speedMultiplier;
    protected float maxHPMultiplier;
    #endregion

    #region Current Stats
    public int attack = 10;
    public int defense = 10;
    public int specialAttack = 10;
    public int specialDefense = 10;
    public int speed = 10;
    public int maxHP = 50;
    public int currentHP = 50;
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
        Rock
    };

    public int xpOnKill;

    public SkillsSO[] currentSkills = new SkillsSO[4];

    public bool TakeDamage(int damage)
    { 
        currentHP -= damage;

        if (currentHP <= 0)
            return true;

        else
            return false;
    }

    //updates amount of XP needed for next level
    public void XPToNextLevel()
    {
        /*int result = 1;
        for (int i = 1; i < currentLevel; i++)
        {
            result *= i;
        }*/
        nextLevelXPNeeded = Mathf.RoundToInt(50 + (100 * currentLevel * currentLevel)); //calculation
        if (currentXP >= nextLevelXPNeeded) //if you have more XP than required for NEXT level, level up again
            LevelUp();
    }


    //Level up method - right now stats are directly tied to level & stat multipliers of child class
    public void LevelUp()
    {
        currentLevel++;
        CalculateStats();
        XPToNextLevel(); 
    }

    public void CalculateStats()
    {
        attack = Mathf.FloorToInt(baseAttack + (currentLevel * attackMultiplier));
        defense = Mathf.FloorToInt(baseDefense + (currentLevel * defenseMultiplier));
        specialAttack = Mathf.FloorToInt(baseSpecialAttack + (currentLevel * specialAttackMultiplier));
        specialDefense = Mathf.FloorToInt(baseSpecialDefense + (currentLevel * specialDefenseMultiplier));
        speed = Mathf.FloorToInt(baseSpeed + (currentLevel * speedMultiplier));
        maxHP = Mathf.FloorToInt(baseMaxHP + (currentLevel * maxHPMultiplier));
    }
}

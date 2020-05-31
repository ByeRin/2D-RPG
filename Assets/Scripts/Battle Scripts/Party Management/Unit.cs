using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<UnitStatsScriptableObject> allPossibleUnits; //List of all possible units
    public UnitStatsScriptableObject currentUnit;

    [Header("Stats")]
    #region Stats
    public int currentLevel;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public int maxHP;
    public int currentHP;
    public int xpOnKill;
    public int currentXP = 0;
    public int nextLevelXPNeeded = 50;
    #endregion

    [Header("Skills")]
    public SkillsSO[] currentSkills = new SkillsSO[4];

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;

        else
            return false;
    }

    protected void StartingSkills()
    {
        for (int i = 0; i < currentSkills.Length; i++)
        {
            if (currentLevel >= currentUnit.levelToLearnSkills[i]) //if an array slot is empty, add the skill
            {
                currentSkills[i] = currentUnit.allSkills[i];
            }
        }
    }
}

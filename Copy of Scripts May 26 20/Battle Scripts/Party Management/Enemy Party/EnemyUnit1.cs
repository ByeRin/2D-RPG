using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit1 : MonoBehaviour
{
    public List<UnitStatsScriptableObject> allPossibleUnits; //List of all possible units
    public UnitStatsScriptableObject currentUnit;
    

    [Header("Stats")]
    public int currentLevel;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public int maxHP;
    public int currentHP;
    public int xpOnKill;

    [Header("Skills")]
    public SkillsSO[] currentSkills = new SkillsSO[4];

    public void OnBattleStart(int activeUnit)
    {
        currentUnit = allPossibleUnits[activeUnit];
        (attack, defense, specialAttack, specialDefense, speed, maxHP) = currentUnit.CalculateStats(currentLevel);
        currentHP = maxHP;
        StartingSkills();
        xpOnKill = (currentLevel / 2) * (attack + defense + specialAttack + specialDefense + speed + maxHP);
        this.GetComponent<SpriteRenderer>().sprite = currentUnit.unitSprite;
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;

        else
            return false;
    }

    void StartingSkills()
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

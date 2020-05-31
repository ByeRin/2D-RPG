using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLord : Unit
{
    void Start()
    {
        unitName = "Fire Lord";
        currentXP = ((currentLevel - 1) * (currentLevel - 1) * 100);
        XPToNextLevel();

        attackMultiplier = 1.5f;
        defenseMultiplier = 2.5f;
        specialAttackMultiplier = 1.5f;
        specialDefenseMultiplier = 2.0f;
        speedMultiplier = 1.7f;
        maxHPMultiplier = 3.7f;

        CalculateStats();

        xpOnKill = (attack + defense + specialAttack + specialDefense + speed + maxHP);
    }
    /*
    public void AddNewSkill(Skills skill)
    {
        for (var i = 0; i < currentSkills.Length; i++)
        {
            if (currentSkills[i] == null)
            {
                currentSkills[i] = skill;
            }
        }
    }
    */
}

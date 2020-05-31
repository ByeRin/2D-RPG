using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadKing : Unit
{
    
    void Start()
    {
        unitName = "Undead King";
        currentXP = ((currentLevel - 1) * (currentLevel - 1) * 100);
        XPToNextLevel();

        attackMultiplier = 2.2f;
        defenseMultiplier = 1.5f;
        specialAttackMultiplier = 1.2f;
        specialDefenseMultiplier = 1.5f;
        speedMultiplier = 1.5f;
        maxHPMultiplier = 3.5f;

        CalculateStats();

        xpOnKill = (attack + defense + specialAttack + specialDefense + speed + maxHP);
    }

}

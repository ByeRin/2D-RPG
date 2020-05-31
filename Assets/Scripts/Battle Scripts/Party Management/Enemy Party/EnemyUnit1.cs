using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit1 : Unit
{
    public void OnBattleStart(int activeUnit)
    {
        currentUnit = allPossibleUnits[activeUnit];
        (attack, defense, specialAttack, specialDefense, speed, maxHP) = currentUnit.CalculateStats(currentLevel);
        currentHP = maxHP;
        StartingSkills();
        xpOnKill = (currentLevel / 2) * (attack + defense + specialAttack + specialDefense + speed + maxHP);
        this.GetComponent<SpriteRenderer>().sprite = currentUnit.unitSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public string unitName;
    public int unitLevel;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int maxHP;
    public int currentHP;

    public List<Skills> currentSkills;

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}

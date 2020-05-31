using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public void Awake()
    {
        currentLevel = 6;
        (attack, defense, specialAttack, specialDefense, speed, maxHP) = currentUnit.CalculateStats(currentLevel);
        currentHP = maxHP;
        StartingSkills();
        XPToNextLevel();
        currentXP = Mathf.RoundToInt(50 + (100 * (currentLevel - 1) * (currentLevel - 1)));
        this.GetComponent<SpriteRenderer>().sprite = currentUnit.unitSprite;
    }

    public void XPToNextLevel()
    {

        nextLevelXPNeeded = Mathf.RoundToInt(50 + (100 * currentLevel * currentLevel)); //calculation
        if (currentXP >= nextLevelXPNeeded) //if you have more XP than required for NEXT level, level up again
            LevelUp();
    }

    //Level up method - right now stats are directly tied to level & stat multipliers of child class
    public void LevelUp()
    {
        currentLevel++;
        currentUnit.CalculateStats(currentLevel);
        LearnNewSkill();
        XPToNextLevel(); //Calculate XP to next level
    }

    //Checks if a new skill is available
    void LearnNewSkill()
    {
        if (currentUnit.levelToLearnSkills.Contains(currentLevel)) //checks second list of Level needed to learn skills
        {
            var index = currentUnit.levelToLearnSkills.IndexOf(currentLevel);
            for (int i = 0; i < currentSkills.Length; i++)
            {
                if (currentSkills[i] == null) //if an array slot is empty, add the skill
                {
                    currentSkills[i] = currentUnit.allSkills[index];
                    return;
                }
            }
            //STILL NEED TO ADD AN OPTION TO CHOOSE TO REPLACE SKILLS
            Debug.Log("Too many Skills");
        }
    }
}

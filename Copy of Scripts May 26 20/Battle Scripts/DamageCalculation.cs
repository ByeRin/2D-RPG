using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalculation : MonoSingleton<DamageCalculation>
{ 
    float attackerTypeMultiplier;

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

    public float CalculateElemental(UnitStatsScriptableObject.ElementTypes attackerElement,
        SkillsSO.ElementTypes skillElement, UnitStatsScriptableObject.ElementTypes defenseElement)
    {
        string attElem = attackerElement.ToString();
        string skillElem = skillElement.ToString();

        //If Attacker is same type as skill used = 50% bonus
        if (attElem == skillElem)
            attackerTypeMultiplier = 1.5f;
        else
            attackerTypeMultiplier = 1f;

        Debug.Log("attacker type modifier = " + attackerTypeMultiplier);

        #region Elemental Type Calculations 
        //NEED ELSE STATEMENTS TO NOT ADD ANY BONUSES ??
        #region NONELEMENTAL Attacks
        if (skillElement == SkillsSO.ElementTypes.NonElemental && (defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Spirit || defenseElement == UnitStatsScriptableObject.ElementTypes.Rock))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region SPIRIT Attacks
        if (skillElement == SkillsSO.ElementTypes.Spirit && (defenseElement == UnitStatsScriptableObject.ElementTypes.Light
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Dark))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Spirit && (defenseElement == UnitStatsScriptableObject.ElementTypes.NonElemental
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Robot))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region FIRE Attacks
        if (skillElement == SkillsSO.ElementTypes.Fire && (defenseElement == UnitStatsScriptableObject.ElementTypes.Earth
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Ice))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Fire && (defenseElement == UnitStatsScriptableObject.ElementTypes.Fire
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Water || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Rock))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region ICE Attacks
        if (skillElement == SkillsSO.ElementTypes.Ice && (defenseElement == UnitStatsScriptableObject.ElementTypes.Earth
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Water))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Ice && (defenseElement == UnitStatsScriptableObject.ElementTypes.Fire
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Ice))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region EARTH Attacks
        if (skillElement == SkillsSO.ElementTypes.Earth && (defenseElement == UnitStatsScriptableObject.ElementTypes.Water
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Rock))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Earth && (defenseElement == UnitStatsScriptableObject.ElementTypes.Fire
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Ice || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Earth))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region LIGHTNING Attacks
        if (skillElement == SkillsSO.ElementTypes.Lightning && (defenseElement == UnitStatsScriptableObject.ElementTypes.Robot
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Water))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Lightning && (defenseElement == UnitStatsScriptableObject.ElementTypes.Rock
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Lightning || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Earth))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region WATER Attacks
        if (skillElement == SkillsSO.ElementTypes.Water && (defenseElement == UnitStatsScriptableObject.ElementTypes.Fire
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Robot || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Rock))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Water && (defenseElement == UnitStatsScriptableObject.ElementTypes.Ice
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Lightning || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Earth))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region LIGHT Attacks
        if (skillElement == SkillsSO.ElementTypes.Light && (defenseElement == UnitStatsScriptableObject.ElementTypes.Dark
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Spirit))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Light && (defenseElement == UnitStatsScriptableObject.ElementTypes.Light
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Robot))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region DARK Attacks
        if (skillElement == SkillsSO.ElementTypes.Dark && (defenseElement == UnitStatsScriptableObject.ElementTypes.Light
            || defenseElement == UnitStatsScriptableObject.ElementTypes.NonElemental))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Dark && (defenseElement == UnitStatsScriptableObject.ElementTypes.Dark
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Robot))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region ROBOT Attacks
        if (skillElement == SkillsSO.ElementTypes.Robot && (defenseElement == UnitStatsScriptableObject.ElementTypes.Light
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Dark || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Spirit))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Robot && (defenseElement == UnitStatsScriptableObject.ElementTypes.Rock
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Lightning || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Water))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #region ROCK Attacks
        if (skillElement == SkillsSO.ElementTypes.Rock && (defenseElement == UnitStatsScriptableObject.ElementTypes.Robot
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Ice))
            return attackerTypeMultiplier * 2f;

        if (skillElement == SkillsSO.ElementTypes.Rock && (defenseElement == UnitStatsScriptableObject.ElementTypes.Water
            || defenseElement == UnitStatsScriptableObject.ElementTypes.Ice || defenseElement ==
            UnitStatsScriptableObject.ElementTypes.Lightning))
            return attackerTypeMultiplier * 0.5f;
        #endregion

        #endregion
        Debug.Log("No additional multipliers");
        return attackerTypeMultiplier;
    }

    public int CalculateDamage(int attack, int skillPower, int level, int defense, float multiplier)
    {
        int damage = Mathf.RoundToInt((((level / 2) + 2) * skillPower * (attack / defense) / 50 + 2) * multiplier);
        return damage;
    }

}

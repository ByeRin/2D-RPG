using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    //setting the battle HUD for the unit ---- USING GAME OBJECTS OLD WAY
    public void SetHUD(PlayerUnit unit)
    {
        nameText.text = unit.currentUnit.unitName;
        levelText.text = "Level: " + unit.currentLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHUD(EnemyUnit1 unit)
    {
        nameText.text = unit.currentUnit.unitName;
        levelText.text = "Level: " + unit.currentLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    //will allow the HP slider to be constantly updated
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

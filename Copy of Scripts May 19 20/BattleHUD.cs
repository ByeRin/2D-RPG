using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    //setting the battle HUD for the unit
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Level: " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    //will allow the HP slider to be constantly updated
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

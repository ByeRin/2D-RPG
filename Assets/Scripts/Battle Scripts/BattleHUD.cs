using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Image hpRadial;
    public TextMeshProUGUI hpText;

    //setting the battle HUD for the unit
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.currentUnit.unitName;
        levelText.text = "Level: " + unit.currentLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpRadial.fillAmount = unit.currentHP / unit.maxHP;
        hpText.text = unit.currentHP.ToString();
    }

    //will allow the HP slider to be constantly updated
    public void SetHP(int hp, int maxHP)
    {
        hpSlider.value = hp;
        hpRadial.fillAmount = (1.0f * hp) / maxHP;
        hpText.text = hp.ToString();
    }
}

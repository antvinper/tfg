using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Slider hpSlider;
    public int maxHP;
    public Image sprite;

    public void SetHUD(Character character)
    {
        nameText.text = character.unitName;
        levelText.text = "Nivel " + character.level;
        hpText.text = "PV: " + character.currentHP + "/" + character.maxHP;
        hpSlider.maxValue = character.maxHP;
        hpSlider.value = character.currentHP;
        sprite.sprite = character.sprite;

        maxHP = character.maxHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpText.text = "PV: " + hp + "/" + maxHP;
    }
}

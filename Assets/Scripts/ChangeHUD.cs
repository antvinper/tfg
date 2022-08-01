using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Image sprite;
    public Text fuerza;
    public Text magia;
    public Text armor;
    public Text magicResist;

    public void SetUpHUD(Character pje)
    {
        nameText.text = pje.unitName;
        levelText.text = "Nivel: " + pje.level;
        hpText.text = "PV: " + pje.currentHP + "/" + pje.maxHP;
        sprite.sprite = pje.sprite;
        fuerza.text = "F: " + pje.attackPower;
        magia.text = "M: " + pje.magicPower;
        armor.text = "A: " + pje.armor;
        magicResist.text = "R: " + pje.magicResist;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipoHUD : MonoBehaviour
{
    public Character pj;

    [Header("Personaje")]
    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Slider hpSlider;
    public Text expText;
    public Slider expSlider;
    public Image sprite;
    public Text fuerza;
    public Text magia;
    public Text armor;
    public Text magicResist;

    [Header("Arma")]
    public Text nameTextWeapon;
    public Text tipoWeapon;
    public Text fuerzaWeapon;
    public Image spriteWeapon;

    public void SetHUDCompleto()
    {
        //Personaje
        nameText.text = pj.name;
        levelText.text = "Nivel " + pj.level;

        hpText.text = "PV: " + pj.currentHP + "/" + pj.maxHP;
        hpSlider.maxValue = pj.maxHP;
        hpSlider.value = pj.currentHP;

        expText.text = "Exp: " + pj.exp + "/100";
        expSlider.maxValue = 100;
        expSlider.value = pj.exp;

        sprite.sprite = pj.sprite;

        fuerza.text = "Fuerza: " + pj.attackPower;
        magia.text = "Magia: " + pj.magicPower;
        armor.text = "Armadura: " + pj.armor;
        magicResist.text = "Resistencia: " + pj.magicResist;

        //Arma
        if(pj.weapon != null)
        {
            nameTextWeapon.text = pj.weapon.name;
            if(pj.weapon.tipo == Tipo.MAGIC)
            {
                tipoWeapon.text = "Tipo: Mágico";
            }else if(pj.weapon.tipo == Tipo.PHYSICAL)
            {
                tipoWeapon.text = "Tipo: Físico";
            }
            fuerzaWeapon.text = "Fuerza " + pj.weapon.fuerza;
            spriteWeapon.sprite = pj.weapon.sprite;
        }
        else
        {
            nameTextWeapon.gameObject.SetActive(false);
            tipoWeapon.gameObject.SetActive(false);
            fuerzaWeapon.gameObject.SetActive(false);
            spriteWeapon.gameObject.SetActive(false);
        }
    }
}

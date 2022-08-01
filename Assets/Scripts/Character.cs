using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public string unitName;
    public int level;

    public int maxHP;
    public int currentHP;

    public int attackPower;
    public int magicPower;
    public int armor;
    public int magicResist;

    public int exp;
    public Sprite sprite;
    public Weapon weapon;

    public bool TakeDamage(int dmg, Weapon arma)
    {
        if(arma == null) {
            int damageTaken = (dmg - armor);
            if (damageTaken < 0) damageTaken = 0;
            currentHP -= damageTaken;

            if (currentHP < 0) currentHP = 0;
        }else if(arma.tipo == Tipo.PHYSICAL)
        {
            int damageTaken = (dmg + arma.fuerza - armor);
            if (damageTaken < 0) damageTaken = 0;
            currentHP -= damageTaken;

            if (currentHP < 0) currentHP = 0;
        }else if(arma.tipo == Tipo.MAGIC)
        {
            int damageTaken = (dmg + arma.fuerza - magicResist);
            if (damageTaken < 0) damageTaken = 0;
            currentHP -= damageTaken;

            if (currentHP < 0) currentHP = 0;
        }

        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void SubeDeNivel(int levelUp, int hpUP, int strUp, int magUp, int armorUp, int mrUp)
    {
        level += levelUp;
        maxHP += hpUP;
        currentHP += hpUP;
        attackPower += strUp;
        magicPower += magUp;
        armor += armorUp;
        magicResist += mrUp;
    }

    public void SetStats(int newLevel, int newMaxHP, int newCurrentHP, int newStr, int newMag, int newArmor, int newRes, int newExp)
    {
        level = newLevel;
        maxHP = newMaxHP;
        currentHP = newCurrentHP;
        attackPower = newStr;
        magicPower = newMag;
        armor = newArmor;
        magicResist = newRes;
        exp = newExp;
    }
}

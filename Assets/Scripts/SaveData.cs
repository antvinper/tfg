using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int worldLevel;

    [Header("Personaje 0")]
    public int level0;
    public int maxHealth0;
    public int currentHealth0;
    public int attackPower0;
    public int magicPower0;
    public int armor0;
    public int magicRes0;
    public int exp0;

    [Header("Personaje 1")]
    public int level1;
    public int maxHealth1;
    public int currentHealth1;
    public int attackPower1;
    public int magicPower1;
    public int armor1;
    public int magicRes1;
    public int exp1;

    [Header("Personaje 2")]
    public int level2;
    public int maxHealth2;
    public int currentHealth2;
    public int attackPower2;
    public int magicPower2;
    public int armor2;
    public int magicRes2;
    public int exp2;

    public SaveData(int progress, Character p0, Character p1, Character p2)
    {
        worldLevel = progress;

        // Personaje 1
        level0 = p0.level;
        maxHealth0 = p0.maxHP;
        currentHealth0 = p0.currentHP;
        attackPower0 = p0.attackPower;
        magicPower0 = p0.magicPower;
        armor0 = p0.armor;
        magicRes0 = p0.magicResist;
        exp0 = p0.exp;

        // Personaje 2
        level1 = p1.level;
        maxHealth1 = p1.maxHP;
        currentHealth1 = p1.currentHP;
        attackPower1 = p1.attackPower;
        magicPower1 = p1.magicPower;
        armor1 = p1.armor;
        magicRes1 = p1.magicResist;
        exp1 = p1.exp;

        // Personaje 3
        level2 = p2.level;
        maxHealth2 = p2.maxHP;
        currentHealth2 = p2.currentHP;
        attackPower2 = p2.attackPower;
        magicPower2 = p2.magicPower;
        armor2 = p2.armor;
        magicRes2 = p2.magicResist;
        exp2 = p2.exp;
    }
}

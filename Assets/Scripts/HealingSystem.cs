using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSystem : MonoBehaviour
{
    public Character player1;
    public Character player2;
    public Character player3;

    public BattleHUD playerHUD1;
    public BattleHUD playerHUD2;
    public BattleHUD playerHUD3;

    public GameObject textoEmergente;
    bool canHeal;


    void Update()
    {
        if (canHeal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HealCharacters();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canHeal = true;
            textoEmergente.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canHeal = false;
            textoEmergente.SetActive(false);
        }
    }

    void HealCharacters()
    {
        player1.currentHP = player1.maxHP;
        player2.currentHP = player2.maxHP;
        player3.currentHP = player3.maxHP;

        playerHUD1.SetHUD(player1);
        playerHUD2.SetHUD(player2);
        playerHUD3.SetHUD(player3);
    }
}

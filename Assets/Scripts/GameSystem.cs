using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Character playerCharacter1;
    public Character playerCharacter2;
    public Character playerCharacter3;

    public BattleHUD playerHUD1;
    public BattleHUD playerHUD2;
    public BattleHUD playerHUD3;

    void Start()
    {
        playerHUD1.SetHUD(playerCharacter1);
        playerHUD2.SetHUD(playerCharacter2);
        playerHUD3.SetHUD(playerCharacter3);
    }

}

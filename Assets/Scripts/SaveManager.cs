using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int progress;
    public Character p0;
    public Character p1;
    public Character p2;

    public void GuardarPartida()
    {
        progress = MainManager.Instance.worldLevel;
        SaveSystem.SaveGame(progress, p0, p1, p2);
    }

    public void CargarPartida()
    {
        SaveData data = SaveSystem.LoadGame();

        progress = data.worldLevel;
        MainManager.Instance.worldLevel = progress;

        // Personaje 1
        p0.SetStats(data.level0, data.maxHealth0, data.currentHealth0, data.attackPower0, data.magicPower0, data.armor0, data.magicRes0, data.exp0);

        // Personaje 2
        p1.SetStats(data.level1, data.maxHealth1, data.currentHealth1, data.attackPower1, data.magicPower1, data.armor1, data.magicRes1, data.exp1);

        // Personaje 3
        p2.SetStats(data.level2, data.maxHealth2, data.currentHealth2, data.attackPower2, data.magicPower2, data.armor2, data.magicRes2, data.exp2);
    }

    public void NuevaPartida()
    {
        progress = 0;

        // Personaje 1
        p0.SetStats(1, 35, 35, 5, 0, 4, 1, 0);

        // Personaje 2
        p1.SetStats(1, 30, 30, 2, 4, 2, 3, 0);

        // Personaje 3
        p2.SetStats(1, 42, 42, 5, 0, 6, 4, 0);
    }
}

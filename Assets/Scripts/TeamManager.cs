using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;

    public Vector3 tempPosition1;
    public Vector3 tempPosition2;

    int actualPos = MainManager.Instance.playerPicker;

    // Cuando se inicializa la escena, comprueba qué personaje dejo el jugador en primer puesto
    public void Start()
    {
        if (actualPos == 0)
        {
            return;
        }
        else if (actualPos == 1)
        {
            SwapPositions();
            actualPos = 1;
            MainManager.Instance.playerPicker = actualPos;
        }
        else if (actualPos == 2)
        {
            SwapPositions();
            SwapPositions();
            actualPos = 2;
            MainManager.Instance.playerPicker = actualPos;
        }
    }

    public void SwapPositions()
    {
        tempPosition1 = a.transform.position;
        tempPosition2 = b.transform.position;

        a.transform.position = c.transform.position;
        b.transform.position = tempPosition1;
        c.transform.position = tempPosition2;

        actualPos++;
        if (actualPos > 2) actualPos = 0;
        MainManager.Instance.playerPicker = actualPos;
    }
}

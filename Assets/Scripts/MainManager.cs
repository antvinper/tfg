using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Vector3 playerPos = new Vector3(0,1, -20.85f);
    public Character[] players = new Character[3];
    public int playerPicker = 0;

    public bool isBoss;
    public int worldLevel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

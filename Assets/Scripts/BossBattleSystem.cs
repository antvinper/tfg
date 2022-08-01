using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattleSystem : MonoBehaviour
{
    public GameObject textoEmergente;
    bool canChallengeBoss;

    void Update()
    {
        if (canChallengeBoss)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None;
                MainManager.Instance.isBoss = true;
                SceneManager.LoadScene("Combate", LoadSceneMode.Single);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canChallengeBoss = true;
            textoEmergente.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canChallengeBoss = false;
            textoEmergente.SetActive(false);
        }
    }
}

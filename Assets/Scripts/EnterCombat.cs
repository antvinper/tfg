using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCombat : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            MainManager.Instance.isBoss = false;
            SceneManager.LoadScene("Combate", LoadSceneMode.Single);
        }
    }
}

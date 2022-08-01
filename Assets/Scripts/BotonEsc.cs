using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEsc : MonoBehaviour
{
    public GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            this.gameObject.SetActive(false);
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

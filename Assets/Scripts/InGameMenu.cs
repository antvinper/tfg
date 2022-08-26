using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject botonEsc;
    GameObject menu;

    public SaveManager saveManager;

    void Start()
    {
        menu = this.gameObject;
        saveManager = GameObject.Find("MainManager").GetComponent<SaveManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
            botonEsc.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }
    }

    public void Guardar()
    {
        saveManager.GuardarPartida();
        Debug.Log("Partida guardada con éxito");
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ConfirmarSalir()
    {
        Application.Quit();
    }

    public void CerrarMenu()
    {
        menu.SetActive(false);
        botonEsc.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }
}

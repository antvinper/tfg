using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SaveManager saveManager;

    public void NewGame()
    {
        Debug.Log("Comenzando una nueva partida");
        saveManager.NuevaPartida();
        SceneManager.LoadScene("Mapa01");
    }

    public void ContinueGame()
    {
        Debug.Log("Cargando datos de la partida guardada");
        saveManager.CargarPartida();
        SceneManager.LoadScene("Mapa01");
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}

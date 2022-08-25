using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public SaveManager saveManager;
    public GameObject textoErrorCarga;

    public void NewGame()
    {
        Debug.Log("Comenzando una nueva partida");
        saveManager.NuevaPartida();
        SceneManager.LoadScene("Mapa01");
    }

    public void ContinueGame()
    {
        Debug.Log("Cargando datos de la partida guardada");
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path))
        {
            saveManager.CargarPartida();
            SceneManager.LoadScene("Mapa01");
        }
        else
        {
            this.gameObject.SetActive(false);
            textoErrorCarga.SetActive(true);
        }
            
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

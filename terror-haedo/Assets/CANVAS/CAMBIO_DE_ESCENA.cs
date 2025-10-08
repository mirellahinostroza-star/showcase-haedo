using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CAMBIO_DE_ESCENA : MonoBehaviour
{
    void Start()
    {
        //  Solo para verificar si la escena existe en el Build Settings
        if (SceneManager.GetSceneByName("NombreDeTuEscena").buildIndex == -1)
        {
            Debug.LogWarning("La escena 'NombreDeTuEscena' no está agregada al Build Settings.");
        }
    }

    public void salir()
    {
        Debug.Log("Salir del Juego");
        Application.Quit();
    }

    public void CambiodeEscena(string NombredeEscena)
    {
        SceneManager.LoadScene(NombredeEscena);
    }
}

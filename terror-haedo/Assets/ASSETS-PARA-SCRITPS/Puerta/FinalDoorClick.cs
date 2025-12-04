using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorClick : MonoBehaviour
{
    private bool finished = false;

    private void OnMouseDown()
    {
        if (finished) return;

        finished = true;
        Debug.Log("[FinalDoor] Puerta clickeada. Cargando escena FINAL...");

        SceneManager.LoadScene("FINAL");
    }
}


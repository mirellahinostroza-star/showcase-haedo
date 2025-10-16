using UnityEngine;

public class MinigameTrigger_RLGL : MonoBehaviour
{
    [SerializeField] private RedLightGreenLightManager rlglManager; // referencia al minijuego Red Light, Green Light
    [SerializeField] private bool deactivateAfterTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rlglManager != null)
            {
                rlglManager.StartMinigame(); // activamos el minijuego
                Debug.Log("▶️ Red Light, Green Light activado!");
            }
            else
            {
                Debug.LogWarning("No se asignó el RedLightGreenLightManager en el trigger.");
            }

            if (deactivateAfterTrigger)
                gameObject.SetActive(false);
        }
    }
}

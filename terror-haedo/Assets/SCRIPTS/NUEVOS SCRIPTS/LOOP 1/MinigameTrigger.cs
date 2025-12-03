using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [SerializeField] private CeilingTrap ceilingTrap; // referencia al minijuego a activar
    [SerializeField] private bool deactivateAfterTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ceilingTrap != null)
            {
                ceilingTrap.StartMinigame();
            }

            if (deactivateAfterTrigger)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
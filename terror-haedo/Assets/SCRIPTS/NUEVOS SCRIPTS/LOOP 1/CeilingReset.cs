using UnityEngine;

public class CeilingReset : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;         
    [SerializeField] private string playerTag = "Player";  
    [SerializeField] private CeilingTrap ceilingTrap;      

    private void OnTriggerEnter(Collider other)
    {
        // Detecta jugador
        if (other.CompareTag(playerTag) || other.transform.root.CompareTag(playerTag))
        {
            // Buscar CharacterController real del jugador
            CharacterController controller = other.GetComponentInParent<CharacterController>();
            Transform playerRoot = controller != null ? controller.transform : other.transform.root;

            if (controller != null)
            {
                controller.enabled = false;
                playerRoot.position = spawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                playerRoot.position = spawnPoint.position;
            }

            Debug.Log("CeilingReset: jugador reposicionado al spawnpoint.");

            // Reiniciar el minijuego para que pueda activarse de nuevo
            if (ceilingTrap != null)
            {
                ceilingTrap.ResetCeiling();
            }
        }
    }
}

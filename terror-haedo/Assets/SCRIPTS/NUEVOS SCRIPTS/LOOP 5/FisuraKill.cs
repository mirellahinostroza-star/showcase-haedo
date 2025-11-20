using UnityEngine;

public class FisuraKill : MonoBehaviour
{
    [Header("Respawn del Loop 5")]
    public Transform spawnPointLoop5; // ← arrastrá acá SpawnPoint_Loop5

    private LoopManager loopManager;

    // Posición original del Fisura
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();

        // Guardar posición inicial
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("☠ El jugador tocó la Fisura. Reiniciando Loop 5...");

        // 1 — Teletransportar al jugador al spawn del Loop5
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        other.transform.position = spawnPointLoop5.position;
        other.transform.rotation = spawnPointLoop5.rotation;

        if (controller != null) controller.enabled = true;

        // 2 — Avisar al LoopManager que reinicie el loop actual
        if (loopManager != null)
        {
            loopManager.ResetCurrentLoop();
            Debug.Log("🔁 Loop reiniciado correctamente");
        }

        // 3 — Resetear el Fisura a su posición original
        ResetFisuraPosition();
    }

    private void ResetFisuraPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Debug.Log("🔄 Fisura reseteado a su posición original.");
    }
}

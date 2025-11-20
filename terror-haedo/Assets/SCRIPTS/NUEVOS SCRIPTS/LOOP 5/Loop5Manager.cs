using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop5Manager : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public WeepingAngelAI angel;           // referencia al Weeping Angel del loop 5
    public GameObject startTrigger;        // trigger para iniciar el loop 5
    public Transform loop5SpawnPoint;

    [Header("Configuración del Loop 5")]
    public bool autoActivateAngel = true;

    void Start()
    {
        ResetLoop5Environment();
    }

    // 👉 Llamado por el trigger de inicio del Loop 5
    public void StartLoop5()
    {
        Debug.Log("[Loop5] Iniciando Loop 5...");

        if (angel != null && autoActivateAngel)
        {
            angel.ActivateAngel();
            Debug.Log("[Loop5] Weeping Angel ACTIVADO");
        }

        if (startTrigger != null)
            startTrigger.SetActive(false);
    }

    // 👉 Llamado cuando el jugador falla o reinicia
    public void ResetLoop5()
    {
        Debug.Log("[Loop5] Reiniciando Loop 5...");

        // Reset del Weeping Angel
        if (angel != null)
        {
            // 🔥 En vez de modificar isActive directo, usamos su método
            angel.DeactivateAngel();

            // Detener movimiento
            var nav = angel.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (nav) nav.ResetPath();
        }

        // Respawn del jugador
        if (loop5SpawnPoint != null)
            loopManager.SetSpawnPoint(loop5SpawnPoint.position);

        loopManager.RespawnPlayer();

        // Reactivar trigger
        if (startTrigger != null)
            startTrigger.SetActive(true);

        Debug.Log("[Loop5] Loop 5 completamente reseteado");
    }

    private void ResetLoop5Environment()
    {
        if (startTrigger != null)
            startTrigger.SetActive(true);
    }
}

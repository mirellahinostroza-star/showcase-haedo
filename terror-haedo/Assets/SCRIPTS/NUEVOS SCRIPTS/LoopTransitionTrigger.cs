using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTransitionTrigger : MonoBehaviour
{
    [Header("Configuración de transición")]
    public LoopManager loopManager;      // referencia al LoopManager
    public int nextLoopIndex;            // índice del loop al que pasará (ej. 2 para Loop 3)
    public Transform nextSpawnPoint;     // spawn del siguiente loop

    private bool triggered = false;      // evita múltiples activaciones

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (loopManager != null && other.gameObject == loopManager.player)
        {
            triggered = true;
            AdvanceToNextLoop();
        }
    }

    private void AdvanceToNextLoop()
    {
        if (loopManager == null)
        {
            Debug.LogError("❌ No se asignó LoopManager en el trigger!");
            return;
        }

        Debug.Log($"⏩ Transicionando al Loop {nextLoopIndex}...");

        // Cambiar punto de respawn al nuevo spawnPoint
        if (nextSpawnPoint != null)
        {
            loopManager.SetSpawnPoint(nextSpawnPoint.position);
        }
        else
        {
            Debug.LogWarning("⚠️ No hay SpawnPoint asignado para el siguiente loop.");
        }

        // Avanzar al siguiente loop
        loopManager.currentLoop = nextLoopIndex;
        loopManager.RespawnPlayer();

        // Activar el nuevo loop
        var loops = loopManager.loops;
        for (int i = 0; i < loops.Length; i++)
        {
            if (loops[i] != null)
                loops[i].SetActive(i == nextLoopIndex);
        }

        Debug.Log($"✅ Loop {nextLoopIndex} activado y jugador movido al SpawnPoint correspondiente.");
    }
    public bool HasBeenTriggered()
{
    return triggered;
}

}


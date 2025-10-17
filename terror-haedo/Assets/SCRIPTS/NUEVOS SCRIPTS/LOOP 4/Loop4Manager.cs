using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop4Manager : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;      // referencia global
    public Light[] lightsToControl;      // luces del pasillo
    public GameObject[] hazardBlocks;    // cubos peligrosos
    public float blackoutDuration = 2f;  // tiempo de oscuridad

    private bool blackoutRunning = false;

    public void StartBlackout()
    {
        if (!blackoutRunning)
            StartCoroutine(BlackoutRoutine());
    }

    IEnumerator BlackoutRoutine()
    {
        blackoutRunning = true;
        Debug.Log("[Loop4] Blackout iniciado");

        // Apagar todas las luces
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = false;

        // Asegurarse de que los cubos estén invisibles
        foreach (var b in hazardBlocks)
            SetBlockVisible(b, false);

        yield return new WaitForSeconds(blackoutDuration);

        // Encender las luces
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = true;

        // Hacer visibles los cubos peligrosos
        foreach (var b in hazardBlocks)
            SetBlockVisible(b, true);

        Debug.Log("[Loop4] Luces encendidas → cubos ahora visibles");
        blackoutRunning = false;
    }

    public void ResetLoop4()
    {
        Debug.Log("[Loop4] Reiniciando Loop 4...");

        // Volver invisibles los cubos
        foreach (var b in hazardBlocks)
            SetBlockVisible(b, false);

        // Respawn del jugador al inicio del Loop 4
        loopManager.RespawnPlayer();
    }

    void SetBlockVisible(GameObject block, bool visible)
    {
        if (block == null) return;

        var renderers = block.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = visible;
    }
}
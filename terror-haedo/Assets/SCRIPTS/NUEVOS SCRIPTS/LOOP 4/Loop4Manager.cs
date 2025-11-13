using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop4Manager : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public Light[] lightsToControl;
    public GameObject[] hazardBlocks;
    public Transform loop4SpawnPoint;
    public GameObject startTrigger; // 👈 referencia al Loop4_StartTrigger

    [Header("Tiempos")]
    public float blackoutDuration = 2f;

    private bool blackoutRunning = false;

    void Start()
    {
        ResetEnvironment();
    }

    public void StartBlackout()
    {
        if (!blackoutRunning)
            StartCoroutine(BlackoutRoutine());
    }

    IEnumerator BlackoutRoutine()
    {
        blackoutRunning = true;
        Debug.Log("[Loop4] Iniciando blackout...");

        // Apagar luces
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = false;

        yield return new WaitForSeconds(blackoutDuration);

        // Encender luces
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = true;

        // Mostrar cubos
        foreach (var b in hazardBlocks)
            SetBlockVisible(b, true);

        Debug.Log("[Loop4] Blackout terminado → luces encendidas y cubos visibles");
        blackoutRunning = false;
    }

    public void ResetLoop4()
    {
        Debug.Log("[Loop4] Jugador falló. Reiniciando Loop 4...");

        // 🔹 Ocultamos los cubos
        foreach (var b in hazardBlocks)
            SetBlockVisible(b, false);

        // 🔹 Reactivamos el trigger de inicio (para que pueda volver a iniciar el blackout)
        if (startTrigger != null)
            startTrigger.SetActive(true);

        // 🔹 Volvemos al punto de respawn del Loop 4
        if (loop4SpawnPoint != null)
            loopManager.SetSpawnPoint(loop4SpawnPoint.position);

        loopManager.RespawnPlayer();

        Debug.Log("[Loop4] Loop reseteado completamente");
    }

    private void ResetEnvironment()
    {
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = true;

        foreach (var b in hazardBlocks)
            SetBlockVisible(b, false);

        if (startTrigger != null)
            startTrigger.SetActive(true);
    }

    private void SetBlockVisible(GameObject block, bool visible)
    {
        if (block == null) return;

        var renderers = block.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = visible;
    }
}
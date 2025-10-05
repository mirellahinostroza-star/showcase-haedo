using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [Header("Loop Control")]
    public int currentLoop = 1;
    public int maxLoops = 5;

    [Header("Player Settings")]
    public GameObject player;
    public Transform entryPoint; // posición de regreso al loop 1

    [Header("Subsystem References")]
    public CeilingTrap ceilingTrap;
    public MemoryFlash memoryFlash;
    public RLGLManager rlglManager;
    public DarkRoomManager darkRoom;
    public GameObject finalExit;

    public void AdvanceLoop()
    {
        currentLoop++;
        if (currentLoop > maxLoops)
        {
            WinGame();
            return;
        }

        Debug.Log("✅ Pasaste al loop " + currentLoop);
        TeleportPlayerTo(entryPoint.position);
    }

    public void FailCurrentLoop()
    {
        Debug.Log("❌ Fallaste en el loop " + currentLoop);
        ResetAllSystems();
        currentLoop = 1;
        TeleportPlayerTo(entryPoint.position);
    }

    public void TeleportPlayerTo(Vector3 pos)
    {
        // Reinicia velocidad/velocidad residual
        var rb = player.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = Vector3.zero;

        player.transform.position = pos;
    }

    void WinGame()
    {
        Debug.Log("🎉 ¡Ganaste el juego!");
        if (finalExit != null) finalExit.SetActive(true);
        // Aquí podrías activar UI de victoria o cargar otra escena
    }

    public void ResetAllSystems()
    {
        ceilingTrap?.ResetTrap();
        memoryFlash?.ResetMiniGame();
        rlglManager?.ResetManager();
        darkRoom?.ResetRoom();
        // Reset visuales globales si los tenés
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFlashMiniGame : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public MemoryFlashBlocker blocker;
    public MemoryFlashLight[] lights;

    [Header("Configuración")]
    public float flashTime = 0.8f;
    public float intervalTime = 0.4f;
    public int sequenceLength = 4;

    private List<MemoryFlashLight> sequence = new List<MemoryFlashLight>();
    private List<MemoryFlashLight> playerInput = new List<MemoryFlashLight>();
    private bool inputEnabled = false;
    private bool inGame = false;

    public void StartMiniGame()
    {
        if (!inGame)
            StartCoroutine(RunMiniGame());
    }

    IEnumerator RunMiniGame()
    {
        inGame = true;
        Debug.Log($"[{gameObject.name}] Iniciando secuencia...");

        sequence.Clear();
        playerInput.Clear();

        // Generar secuencia aleatoria
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(lights[Random.Range(0, lights.Length)]);
        }

        // Mostrar secuencia
        foreach (var l in sequence)
        {
            l.Flash(flashTime);
            yield return new WaitForSeconds(flashTime + intervalTime);
        }

        Debug.Log("[MemoryFlash] Esperando clics del jugador...");
        inputEnabled = true;
    }

    public void RegisterPlayerClick(MemoryFlashLight clickedLight)
    {
        if (!inputEnabled) return;

        playerInput.Add(clickedLight);
        int index = playerInput.Count - 1;

        // Verificar si la entrada es correcta
        if (playerInput[index] != sequence[index])
        {
            Debug.Log("[MemoryFlash] Secuencia incorrecta. Reiniciando Loop actual...");
            inputEnabled = false;
            OnLose();
            return;
        }

        // Si completó toda la secuencia
        if (playerInput.Count == sequence.Count)
        {
            Debug.Log("[MemoryFlash] ¡Secuencia completada!");
            inputEnabled = false;
            OnWin();
        }
    }

    void OnWin()
    {
        Debug.Log($"[{gameObject.name}] Ganaste el Memory Flash!");
        if (blocker != null)
            blocker.UnlockPath();
        inGame = false;
    }

    void OnLose()
    {
        Debug.Log("[MemoryFlash] Fallaste. Reiniciando el loop actual...");
        loopManager.RespawnPlayer(); // vuelve al spawnPoint del loop activo
        inGame = false;
    }
}
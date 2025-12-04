using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFlashMiniGame : MonoBehaviour
{
    public LoopManager loopManager;
    public List<MemoryFlashLight> lights;
    public GameObject blocker;           // 👈 ahora es un ASSET visual, no solo collider
    public float flashDuration = 0.4f;

    private List<int> pattern = new List<int>();
    private int currentIndex = 0;
    private bool gameRunning = false;

    void Start()
    {
        // Asegurar que el blocker comience activo
        if (blocker != null)
            blocker.SetActive(true);
    }

    public void StartMiniGame()
    {
        Debug.Log("[Loop2] Comenzando Memory Flash…");

        pattern.Clear();
        currentIndex = 0;
        gameRunning = false;

        // Crear patrón de 4 luces
        for (int i = 0; i < 4; i++)
            pattern.Add(Random.Range(0, lights.Count));

        StartCoroutine(ShowPattern());
    }

    IEnumerator ShowPattern()
    {
        gameRunning = false;

        foreach (int index in pattern)
        {
            lights[index].Flash(flashDuration);
            yield return new WaitForSeconds(flashDuration + 0.2f);
        }

        gameRunning = true;
        currentIndex = 0;
    }

    // Llamado por cada luz cuando el jugador hace click
    public void RegisterPlayerClick(MemoryFlashLight clickedLight)
    {
        if (!gameRunning) return;

        int index = lights.IndexOf(clickedLight);

        // ✔ Correcto
        if (index == pattern[currentIndex])
        {
            currentIndex++;

            // ✔ COMPLETÓ EL PATRÓN
            if (currentIndex >= pattern.Count)
                WinMiniGame();

            return;
        }

        // ❌ Incorrecto
        LoseMiniGame();
    }

    void WinMiniGame()
    {
        gameRunning = false;
        Debug.Log("[Loop2] ¡Ganaste el Memory Flash!");

        // 🔥 DESAPARECE EL ASSET bloqueador
        if (blocker != null)
            blocker.SetActive(false);
    }

    void LoseMiniGame()
    {
        gameRunning = false;

        Debug.Log("[Loop2] Fallaste el Memory Flash → Reiniciando Loop 2");
        loopManager.ResetToFirstLoop();
    }
}
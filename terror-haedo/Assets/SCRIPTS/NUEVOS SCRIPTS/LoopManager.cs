using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoopManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;
    public Transform spawnPoint;

    [Header("Loops")]
    public GameObject[] loops; // ← arrastrá aquí tus loops en orden (Loop1, Loop2, etc.)

    [HideInInspector] public int currentLoop = 0;
    private Vector3 initialSpawnPosition;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (spawnPoint != null)
            initialSpawnPosition = spawnPoint.position;
        else
            initialSpawnPosition = player.transform.position;

        ActivateLoop(currentLoop);
    }

        // 🔁 Resetear al primer loop
    public void ResetToFirstLoop()
    {
        Debug.Log("🔄 Reset to first loop");
        currentLoop = 0;
        RespawnPlayer();
        ActivateLoop(currentLoop);
    }


    // ⏩ Avanzar al siguiente loop
    public void AdvanceLoop()
    {
        currentLoop++;
        if (currentLoop >= loops.Length)
        {
            Debug.Log("🎉 ¡Ganaste todos los loops!");
            currentLoop = loops.Length - 1; // se queda en el último
            return;
        }

        Debug.Log("⏭ Avanzando al loop " + currentLoop);
        RespawnPlayer();
        ActivateLoop(currentLoop);
    }

    // 🧍‍♂️ Respawn del jugador
    public void RespawnPlayer()
    {
        if (player == null) return;

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = initialSpawnPosition;
            controller.enabled = true;
        }
        else
        {
            player.transform.position = initialSpawnPosition;
        }

        Debug.Log("Jugador respawneado en el punto inicial");
    }

    // 📍 Cambiar punto de respawn dinámicamente
    public void SetSpawnPoint(Vector3 newSpawn)
    {
        initialSpawnPosition = newSpawn;
    }

    // 🟢 Activa solo el loop actual
    private void ActivateLoop(int index)
    {
        for (int i = 0; i < loops.Length; i++)
        {
            if (loops[i] != null)
                loops[i].SetActive(i == index);
        }

        Debug.Log("Loop activo: " + index);
    }
    public void ResetCurrentLoop()
{
    Debug.Log($"🔁 Reiniciando loop actual: {currentLoop}");
    RespawnPlayer();
}
}
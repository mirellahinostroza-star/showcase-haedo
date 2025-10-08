using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoopManager : MonoBehaviour
{
    [Header("Configuración general")]
    public GameObject player;              // tu prefab de Player del asset store
    public GameObject[] loops;             // tus escenarios o prefabs de cada loop (loop1, loop2, etc.)
    public int currentLoop = 1;            // loop actual (comienza en 1)
    public int maxLoops = 5;               // máximo de loops (en tu caso, 5)

    private Vector3 entryPoint;            // posición inicial del jugador

    void Start()
    {
        // Guarda la posición inicial del jugador al iniciar el juego
        if (player != null)
            entryPoint = player.transform.position;

        // Asegura que solo el loop 1 esté activo al inicio
        for (int i = 0; i < loops.Length; i++)
        {
            loops[i].SetActive(i == 0);
        }
    }

    // Cuando el jugador completa un loop correctamente
    public void AdvanceLoop()
    {
        if (currentLoop < maxLoops)
        {
            loops[currentLoop - 1].SetActive(false);  // desactiva el loop anterior
            loops[currentLoop].SetActive(true);       // activa el siguiente loop
            currentLoop++;

            Debug.Log("Avanzaste al loop " + currentLoop);
        }
        else
        {
            Debug.Log("¡Ganaste el juego! Llegaste al loop final.");
            // Podés agregar una animación de salida, pantalla de victoria, etc.
        }
    }

    // Cuando el jugador falla un desafío o toca un hazard
    public void ResetToFirstLoop()
    {
        Debug.Log("Fallaste. Volviendo al loop 1...");

        // Desactiva todos los loops excepto el primero
        for (int i = 0; i < loops.Length; i++)
        {
            loops[i].SetActive(i == 0);
        }

        currentLoop = 1;

        // Teletransporta al jugador a la posición inicial
        TeleportPlayerTo(entryPoint);
    }

    // Teletransporta al jugador sin romper su control de cámara
    public void TeleportPlayerTo(Vector3 position)
    {
        if (player == null) return;

        // Si tiene Rigidbody, detenemos su movimiento
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.transform.position = position;
    }
}
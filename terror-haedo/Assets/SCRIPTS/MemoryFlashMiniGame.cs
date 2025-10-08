using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFlashMiniGame : MonoBehaviour
{
    public LoopManager loopManager;
    private bool inGame = false;
    private bool miniGameWon = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == loopManager.player && !inGame)
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        inGame = true;
        Debug.Log("Iniciando Memory Flash...");
        // Aquí mostrarías tu UI de minijuego real

        // --- Simulación de resultado ---
        bool success = Random.value > 0.5f; // resultado aleatorio temporal

        if (success)
            OnWin();
        else
            OnLose();
    }

    void OnWin()
    {
        Debug.Log("Ganaste el Memory Flash!");
        miniGameWon = true;
        loopManager.AdvanceLoop();
        inGame = false;
    }

    void OnLose()
    {
        Debug.Log("Perdiste el Memory Flash!");
        loopManager.ResetToFirstLoop();
        inGame = false;
    }
}
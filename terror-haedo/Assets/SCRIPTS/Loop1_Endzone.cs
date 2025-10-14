using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop1_EndZone : MonoBehaviour
{
    public LoopManager loopManager;
    public Loop1CeilingTrap ceilingTrap; // opcional, por si querés detener el techo visualmente

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Loop1] Jugador llegó al final, pasando al Loop 2...");
            
            if (ceilingTrap != null)
                ceilingTrap.StopTrap(); // Detiene el techo, opcional
            
            loopManager.AdvanceLoop();
            loopManager.RespawnPlayer(); // vuelve al punto inicial
        }
    }
}
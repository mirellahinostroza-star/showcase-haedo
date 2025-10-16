using UnityEngine;

public class StopCeilingByLoop : MonoBehaviour
{
    [Header("Ceiling Trap")]
    public CeilingTrap ceilingTrap;

    [Header("Loop Manager")]
    public LoopManager loopManager;

    [Header("Loop en el que debe estar activo")]
    public int activeLoopIndex = 0; // Loop1 = 0

    void Update()
    {
        if (ceilingTrap == null || loopManager == null) return;

        // Detener CeilingTrap si el jugador no está en el loop activo
        if (loopManager.currentLoop != activeLoopIndex && ceilingTrap != null)
        {
            ceilingTrap.ResetCeiling();
        }
    }
}
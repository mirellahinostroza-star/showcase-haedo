using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightGreenLightManager : MonoBehaviour
{
    [Header("Referencias generales")]
    public List<DollController> dolls; 
    public float greenDuration = 3f;
    public float redDuration = 2f;

    [HideInInspector] public bool isRedLight = false;

    private Coroutine cycleCoroutine;
    private bool isActive = false;

    // Llamado desde el trigger para arrancar el minijuego
    public void StartMinigame()
    {
        if (isActive) return; // si ya está activo, no reiniciar

        isActive = true;
        isRedLight = false;

        foreach (var doll in dolls)
            doll.SetLightState(true);

        cycleCoroutine = StartCoroutine(Cycle());
    }

    // Llamado cuando el jugador muere
    public void StopMinigame()
    {
        if (!isActive) return;

        isActive = false;
        isRedLight = false;

        if (cycleCoroutine != null)
            StopCoroutine(cycleCoroutine);

        // Dejar muñecas en verde al detener
        foreach (var doll in dolls)
            doll.SetLightState(true);
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            // 🟢 GREEN LIGHT
            isRedLight = false;
            foreach (var doll in dolls)
                doll.SetLightState(true);

            yield return new WaitForSeconds(greenDuration);

            // 🔴 RED LIGHT
            isRedLight = true;
            foreach (var doll in dolls)
                doll.SetLightState(false);

            yield return new WaitForSeconds(redDuration);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnLoop2 : MonoBehaviour
{
    public LoopManager loopManager; // arrastrá tu LoopManager aquí

    void Start()
    {
        if (loopManager == null)
        {
            Debug.LogWarning("[ShowOnLoop2] No se asignó LoopManager");
            return;
        }

        // Oculta MEMORY FLASH al inicio
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (loopManager.currentLoop == 1 && !gameObject.activeSelf) // Loop 2
        {
            gameObject.SetActive(true); // muestra MEMORY FLASH
            Debug.Log("[Loop2] MEMORY FLASH activado al entrar al Loop 2");
        }
    }
}
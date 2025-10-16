using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MemoryFlashTrigger : MonoBehaviour
{
    public MemoryFlashMiniGame miniGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[{gameObject.name}] Jugador entró al trigger. Iniciando Memory Flash...");
            miniGame.StartMiniGame();
        }
    }
}
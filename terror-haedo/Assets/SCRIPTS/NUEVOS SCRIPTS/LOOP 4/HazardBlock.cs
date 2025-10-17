using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    private Loop4Manager loop4Manager;

    void Start()
    {
        loop4Manager = FindObjectOfType<Loop4Manager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta el choque físico con el jugador
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("[Loop4] Jugador chocó con un bloque peligroso!");
            loop4Manager.ResetLoop4();
        }
    }
}
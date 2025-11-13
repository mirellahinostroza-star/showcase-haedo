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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Loop4] Jugador tocó un bloque peligroso (Trigger)!");
            loop4Manager.ResetLoop4();
        }
    }
}
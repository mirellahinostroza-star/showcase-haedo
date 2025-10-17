using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loop4TriggerStart : MonoBehaviour
{
    public Loop4Manager loop4Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Loop4] Jugador entró al trigger de inicio → iniciando blackout");
            loop4Manager.StartBlackout();
            gameObject.SetActive(false); // evita que se repita el evento
        }
    }
}
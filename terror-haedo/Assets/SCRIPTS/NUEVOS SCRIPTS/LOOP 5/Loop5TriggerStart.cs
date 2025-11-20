using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop5TriggerStart : MonoBehaviour
{
    public Loop5Manager loop5Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Loop5] Jugador activó el trigger → iniciando Loop 5");
            loop5Manager.StartLoop5();
            gameObject.SetActive(false);
        }
    }
}

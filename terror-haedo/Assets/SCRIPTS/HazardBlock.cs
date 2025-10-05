using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    public DarkRoomManager darkRoomManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            darkRoomManager.PlayerHitHazard();
    }
}
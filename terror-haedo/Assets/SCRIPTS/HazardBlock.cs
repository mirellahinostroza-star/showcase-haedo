using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    public DarkRoomManager darkRoomManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == darkRoomManager.loopManager.player)
        {
            darkRoomManager.PlayerHitHazard();
        }
    }
}
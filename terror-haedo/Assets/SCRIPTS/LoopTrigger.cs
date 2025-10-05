using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTrigger : MonoBehaviour
{
    public MemoryFlash memoryFlash;
    public RLGLManager rlglManager;
    public DarkRoomManager darkRoomManager;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || activated) return;
        activated = true;

        if (memoryFlash != null)
            memoryFlash.StartMiniGame(FindObjectOfType<LoopManager>());
        if (rlglManager != null)
            rlglManager.StartRLGL();
        if (darkRoomManager != null)
            darkRoomManager.StartDarkRoom();
    }
}
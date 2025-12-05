using UnityEngine;

public class MemoryFlashBlocker : MonoBehaviour
{
    public void UnlockPath()
    {
        Debug.Log($"[{gameObject.name}] Camino desbloqueado.");
        gameObject.SetActive(false);
    }
}

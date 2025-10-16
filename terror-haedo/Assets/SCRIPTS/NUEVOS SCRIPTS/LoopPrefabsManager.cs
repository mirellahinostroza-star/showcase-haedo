using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoopPrefabGroup
{
    public string loopName;       // Solo para identificar en el inspector
    public GameObject[] prefabs;  // Prefabs de este loop
}

public class LoopPrefabsManager : MonoBehaviour
{
    [Header("Loop Manager")]
    public LoopManager loopManager;

    [Header("Prefabs por Loop (1-5)")]
    public List<LoopPrefabGroup> loops = new List<LoopPrefabGroup>();

    private int currentActiveLoop = -1;

    private void OnEnable()
    {
        // Espera un frame para asegurarse de que LoopManager ya inicializó currentLoop
        StartCoroutine(InitPrefabsNextFrame());
    }

    private IEnumerator InitPrefabsNextFrame()
    {
        yield return null; // espera un frame
        if (loopManager != null)
        {
            UpdatePrefabs(loopManager.currentLoop);
            currentActiveLoop = loopManager.currentLoop;
        }
    }

    private void Update()
    {
        if (loopManager == null || loops == null) return;

        int loopIndex = loopManager.currentLoop;

        // Solo actualiza si cambió el loop
        if (loopIndex != currentActiveLoop)
        {
            UpdatePrefabs(loopIndex);
            currentActiveLoop = loopIndex;
        }
    }

    private void UpdatePrefabs(int activeLoopIndex)
    {
        for (int i = 0; i < loops.Count; i++)
        {
            bool shouldShow = (i == activeLoopIndex);

            if (loops[i].prefabs == null) continue;

            foreach (GameObject obj in loops[i].prefabs)
            {
                if (obj != null)
                    obj.SetActive(shouldShow);
            }
        }
    }
}

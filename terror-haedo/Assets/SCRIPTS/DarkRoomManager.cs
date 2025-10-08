using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoomManager : MonoBehaviour
{
    public LoopManager loopManager;
    public Light mainLight;
    public GameObject[] hazardBlocks;

    private bool inDarkSequence = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == loopManager.player && !inDarkSequence)
        {
            StartCoroutine(DarkSequence());
        }
    }

    System.Collections.IEnumerator DarkSequence()
    {
        inDarkSequence = true;
        Debug.Log("Luces fuera...");

        // Fade out
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            mainLight.intensity = i;
            yield return null;
        }

        // Luces fuera
        yield return new WaitForSeconds(1f);

        // Luces on + hazards activos
        foreach (var h in hazardBlocks)
            h.SetActive(true);

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            mainLight.intensity = i;
            yield return null;
        }

        Debug.Log("Evita los bloques peligrosos...");
    }

    public void PlayerHitHazard()
    {
        Debug.Log("Tocaste un bloque peligroso!");
        loopManager.ResetToFirstLoop();
    }

    public void PlayerReachedExit()
    {
        loopManager.AdvanceLoop();
    }
}
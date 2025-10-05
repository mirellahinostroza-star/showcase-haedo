using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoomManager : MonoBehaviour
{
    public LoopManager loopManager;
    public Light[] lights;
    public GameObject[] hazardBlocks;
    public Transform goal;
    public Transform player;

    private bool gameActive = false;

    public void StartDarkRoom()
    {
        if (!gameActive)
            StartCoroutine(DarkSequence());
    }

    private IEnumerator DarkSequence()
    {
        gameActive = true;

        // Fade out
        foreach (var l in lights) l.intensity = 0f;
        yield return new WaitForSeconds(1.5f);

        // Aparecen bloques
        foreach (var b in hazardBlocks) b.SetActive(true);

        // Flash de luz repentino
        foreach (var l in lights) l.intensity = 1f;
        Debug.Log("⚠ Evita los bloques");

        // Esperar hasta llegar a meta o tocar un bloque
        while (Vector3.Distance(player.position, goal.position) > 1f)
        {
            yield return null;
        }

        // Si llega a meta
        loopManager.AdvanceLoop();
        gameActive = false;
    }

    public void PlayerHitHazard()
    {
        loopManager.FailCurrentLoop();
        gameActive = false;
    }

    public void ResetRoom()
    {
        StopAllCoroutines();
        gameActive = false;
        foreach (var b in hazardBlocks) b.SetActive(false);
        foreach (var l in lights) l.intensity = 1f;
    }
}
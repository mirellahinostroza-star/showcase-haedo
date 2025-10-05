using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryFlash : MonoBehaviour
{
    [Header("Lights / Panels")]
    public Light[] flashLights;
    public float flashDelay = 0.6f;
    public int sequenceLength = 4;

    private List<int> sequence = new List<int>();
    private int currentIndex = 0;
    private bool playerInputActive = false;
    private LoopManager loopManager;

    public void StartMiniGame(LoopManager manager)
    {
        loopManager = manager;
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
            sequence.Add(Random.Range(0, flashLights.Length));

        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            flashLights[index].enabled = true;
            yield return new WaitForSeconds(flashDelay);
            flashLights[index].enabled = false;
            yield return new WaitForSeconds(0.3f);
        }

        currentIndex = 0;
        playerInputActive = true;
        Debug.Log("🎮 Repite la secuencia de luces");
    }

    public void PlayerPress(int lightIndex)
    {
        if (!playerInputActive) return;

        flashLights[lightIndex].enabled = true;
        StartCoroutine(TurnOffLight(flashLights[lightIndex], 0.2f));

        if (lightIndex == sequence[currentIndex])
        {
            currentIndex++;
            if (currentIndex >= sequence.Count)
            {
                playerInputActive = false;
                loopManager.AdvanceLoop();
            }
        }
        else
        {
            playerInputActive = false;
            loopManager.FailCurrentLoop();
        }
    }

    IEnumerator TurnOffLight(Light l, float delay)
    {
        yield return new WaitForSeconds(delay);
        l.enabled = false;
    }

    public void ResetMiniGame()
    {
        StopAllCoroutines();
        sequence.Clear();
        playerInputActive = false;
        foreach (var l in flashLights) l.enabled = false;
    }
}
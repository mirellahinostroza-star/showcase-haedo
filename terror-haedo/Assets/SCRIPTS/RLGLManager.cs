using System.Collections;
using UnityEngine;
using System.Collections;

public class RLGLManager : MonoBehaviour
{
    public Transform player;
    public Transform goal;
    public LoopManager loopManager;
    public int rounds = 3;
    public float greenMin = 2f, greenMax = 4f;
    public float redMin = 1.5f, redMax = 3f;
    public float moveThreshold = 0.1f;

    private bool gameActive = false;

    public void StartRLGL()
    {
        if (!gameActive)
            StartCoroutine(RLGLSequence());
    }

    private IEnumerator RLGLSequence()
    {
        gameActive = true;

        for (int i = 0; i < rounds; i++)
        {
            float greenTime = Random.Range(greenMin, greenMax);
            Debug.Log("🟩 Verde - Avanza!");
            yield return new WaitForSeconds(greenTime);

            float redTime = Random.Range(redMin, redMax);
            Vector3 lastPos = player.position;
            Debug.Log("🟥 Rojo - Quieto!");
            float elapsed = 0f;
            while (elapsed < redTime)
            {
                elapsed += Time.deltaTime;
                if (Vector3.Distance(player.position, lastPos) > moveThreshold)
                {
                    loopManager.FailCurrentLoop();
                    gameActive = false;
                    yield break;
                }
                yield return null;
            }
        }

        if (Vector3.Distance(player.position, goal.position) < 2f)
            loopManager.AdvanceLoop();
        else
            loopManager.FailCurrentLoop();

        gameActive = false;
    }

    public void ResetManager()
    {
        StopAllCoroutines();
        gameActive = false;
    }
}
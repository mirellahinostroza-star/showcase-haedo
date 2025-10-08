using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenMiniGame : MonoBehaviour
{
    public LoopManager loopManager;
    public float redLightDuration = 3f;
    public float greenLightDuration = 3f;

    private bool inGame = false;
    private bool isRedLight = false;
    private Rigidbody playerRb;
    private Vector3 lastPos;

    void Start()
    {
        playerRb = loopManager.player.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == loopManager.player && !inGame)
        {
            StartCoroutine(GameSequence());
        }
    }

    System.Collections.IEnumerator GameSequence()
    {
        inGame = true;
        Debug.Log("Comienza Luz Roja, Luz Verde...");

        for (int i = 0; i < 3; i++) // 3 rondas
        {
            // Verde
            isRedLight = false;
            Debug.Log("🟢 Verde!");
            yield return new WaitForSeconds(greenLightDuration);

            // Roja
            isRedLight = true;
            lastPos = loopManager.player.transform.position;
            Debug.Log("🔴 Roja!");
            yield return new WaitForSeconds(redLightDuration);

            // Si se movió durante la luz roja, pierde
            if (Vector3.Distance(lastPos, loopManager.player.transform.position) > 0.5f)
            {
                Debug.Log("¡Te moviste en luz roja!");
                loopManager.ResetToFirstLoop();
                inGame = false;
                yield break;
            }
        }

        Debug.Log("Superaste Luz Roja, Luz Verde!");
        loopManager.AdvanceLoop();
        inGame = false;
    }
}
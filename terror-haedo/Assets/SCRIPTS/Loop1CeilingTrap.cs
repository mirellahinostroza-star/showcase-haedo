using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loop1CeilingTrap : MonoBehaviour
{
    public LoopManager loopManager;  
    public Transform ceiling;
    public Transform ceilingStart;
    public Transform ceilingEnd;
  
    public float baseSpeed = 0.2f;       
    public float speedMultiplier = 0.8f;  
    public float delayBeforeDrop = 1.5f;

    private bool triggered = false;
    private bool isMoving = false;
    private bool playerHit = false;

    void Start()
    {
        // Asegura que el techo arranque arriba
        if (ceilingStart != null && ceiling != null)
            ceiling.position = ceilingStart.position;
    }

    void Update()
    {
        if (!isMoving) return;

        GameObject player = loopManager.player;
        if (player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        float playerSpeed = rb != null ? rb.velocity.magnitude : 0f;

        float moveSpeed = baseSpeed + playerSpeed * speedMultiplier;

        ceiling.position = Vector3.MoveTowards(
            ceiling.position,
            ceilingEnd.position,
            moveSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Si aún no se activó, inicia el movimiento del techo
        if (!triggered)
        {
            triggered = true;
            StartCoroutine(StartCeilingAfterDelay());
        }
        // Si el techo ya está bajando y toca al jugador
        else if (isMoving)
        {
            playerHit = true;
            Debug.Log("[Loop1] El techo tocó al jugador. Reiniciando loop...");
            StopTrapAndReset();
            loopManager.ResetToFirstLoop();
        }
    }

    IEnumerator StartCeilingAfterDelay()
    {
        Debug.Log("[Loop1] Trigger activado, esperando antes de bajar...");
        yield return new WaitForSeconds(delayBeforeDrop);
        isMoving = true;
        Debug.Log("[Loop1] El techo comienza a bajar");
    }

    public void StopTrapAndReset()
    {
        StopAllCoroutines();
        triggered = false;
        isMoving = false;
        playerHit = false;

        if (ceilingStart != null && ceiling != null)
            ceiling.position = ceilingStart.position;

        Debug.Log("[Loop1] Techo reseteado a posición inicial.");
    }
    public void StopTrap()
{
    StopTrapAndReset();
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop1CeilingTrap : MonoBehaviour
{
    public LoopManager loopManager;
    public Transform ceiling;
    public Transform ceilingStart;
    public Transform ceilingEnd;

    [Header("Velocidades")]
    public float baseSpeed = 0.2f;         // velocidad base del techo
    public float speedMultiplier = 1.5f;   // cuanto más rápido baja cuando el jugador corre
    public float maxSpeed = 3f;            // límite de velocidad para evitar que sea ridículo

    [Header("Delay")]
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

    float moveSpeed = baseSpeed;

    // Si el jugador presiona Shift, aumenta velocidad y muestra debug
    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    {
        moveSpeed *= 4f;
        Debug.Log("Jugador corriendo, techo bajando rápido");
    }

    // Baja el techo indefinidamente
    ceiling.position += Vector3.down * moveSpeed * Time.deltaTime;

    // Detectar colisión con el jugador manualmente
    if (!playerHit && loopManager.player != null)
    {
        Collider playerCollider = loopManager.player.GetComponent<Collider>();
        Collider ceilingCollider = ceiling.GetComponent<Collider>();

        if (playerCollider != null && ceilingCollider != null)
        {
            if (ceilingCollider.bounds.Intersects(playerCollider.bounds))
            {
                playerHit = true;
                Debug.Log("[Loop1] El techo tocó al jugador. Reiniciando loop...");
                StopTrapAndReset();
                loopManager.ResetToFirstLoop();
            }
        }
    }
}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!triggered)
        {
            triggered = true;
            StartCoroutine(StartCeilingAfterDelay());
        }
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
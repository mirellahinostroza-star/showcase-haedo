using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop1CeilingTrap : MonoBehaviour
{
    public LoopManager loopManager;   // asignar en inspector
    public Transform ceiling;
    public Transform ceilingStart;
    public Transform ceilingEnd;
    public float baseSpeed = 2f;

    private bool triggered = false;

    void Start()
    {
        if (ceilingStart != null && ceiling != null)
            ceiling.position = ceilingStart.position;
    }

    void Update()
    {
        if (!triggered) return;

        GameObject player = loopManager.player;
        if (player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        float playerSpeed = rb != null ? rb.velocity.magnitude : 0f;
        float speed = baseSpeed + playerSpeed * 1.5f;

        ceiling.position = Vector3.MoveTowards(ceiling.position, ceilingEnd.position, speed * Time.deltaTime);

        // Si el techo llegó al final -> fallo
        if (Vector3.Distance(ceiling.position, ceilingEnd.position) < 0.05f)
        {
            triggered = false;
            loopManager.ResetToFirstLoop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Loop1] Start trigger activated by Player");
            triggered = true;
        }
    }

    // Llamar esta función si el jugador alcanza la zona final segura
    public void StopTrapAndAdvance()
    {
        triggered = false;
        if (ceilingStart != null)
            ceiling.position = ceilingStart.position;
        loopManager.AdvanceLoop();
    }

    public void ResetTrap()
    {
        triggered = false;
        if (ceilingStart != null)
            ceiling.position = ceilingStart.position;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTrap : MonoBehaviour
{
    [Header("Ceiling Settings")]
    public Transform ceiling;
    public float baseSpeed = 1.5f;
    public float maxSpeed = 6f;
    public float speedNormalization = 5f; // divide velocidad jugador
    public float safeOffset = 1f;

    [Header("Player Reference")]
    public Transform player;
    public CharacterController playerController;

    [Header("Loop Manager")]
    public LoopManager loopManager;

    [Header("End Zone")]
    public Collider endZone;

    private bool trapActive = false;
    private Vector3 ceilingStartPos;

    void Start()
    {
        ceilingStartPos = ceiling.position;
    }

    void Update()
    {
        if (!trapActive) return;

        float playerSpeed = playerController.velocity.magnitude;
        float t = Mathf.Clamp01(playerSpeed / speedNormalization);
        float speed = Mathf.Lerp(baseSpeed, maxSpeed, t);

        ceiling.Translate(Vector3.down * speed * Time.deltaTime);

        if (ceiling.position.y <= player.position.y + safeOffset)
        {
            trapActive = false;
            loopManager.FailCurrentLoop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !trapActive)
        {
            trapActive = true;
        }

        if (other == endZone)
        {
            trapActive = false;
            loopManager.AdvanceLoop();
        }
    }

    public void ResetTrap()
    {
        trapActive = false;
        ceiling.position = ceilingStartPos;
    }
}
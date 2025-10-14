using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisuraLevel : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public Transform cube;       // El cubo que te observa
    public Transform player;     // El jugador
    public Transform exitPoint;  // Meta final

    [Header("Configuración")]
    public float chaseSpeed = 5f;
    public float detectionAngle = 40f;     // Ángulo de visión permitido
    public float distanceThreshold = 1.5f; // Distancia a la que el cubo “mata”
    public float checkInterval = 0.2f;     // Cada cuánto verifica si lo mirás

    private bool cubeAwakened = false;
    private bool chasing = false;

    void Awake()
    {
        if (loopManager == null)
            loopManager = FindObjectOfType<LoopManager>();
    }

    void Start()
    {
        if (player == null && loopManager != null)
            player = loopManager.player.transform;

        StartCoroutine(CheckPlayerGaze());
    }

    IEnumerator CheckPlayerGaze()
    {
        while (!chasing)
        {
            Vector3 dirToCube = (cube.position - player.position).normalized;
            float angle = Vector3.Angle(player.forward, dirToCube);

            // Si el jugador sigue mirando al cubo
            if (angle < detectionAngle)
            {
                yield return new WaitForSeconds(checkInterval);
                continue;
            }

            // Dejó de mirarlo
            Debug.Log("🧠 La fisura se abre... dejaste de mirar al cubo.");
            StartCoroutine(StartChase());
            yield break;
        }
    }

    IEnumerator StartChase()
    {
        chasing = true;
        cubeAwakened = true;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        while (cubeAwakened)
        {
            Vector3 dir = (player.position - cube.position).normalized;
            cube.position += dir * chaseSpeed * Time.deltaTime;

            // Si el cubo alcanza al jugador
            if (Vector3.Distance(cube.position, player.position) < distanceThreshold)
            {
                Debug.Log("💀 El cubo te atrapó dentro de la fisura.");
                cubeAwakened = false;
                loopManager.ResetToFirstLoop();
                yield break;
            }

            yield return null;
        }
    }

    public void PlayerReachedExit()
    {
        if (chasing)
            Debug.Log("🏃 Escapaste de la fisura... sobreviviste.");

        cubeAwakened = false;
        chasing = false;

        // Ganó el juego o pasó al final
        loopManager.AdvanceLoop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == player)
        {
            PlayerReachedExit();
        }
    }
}
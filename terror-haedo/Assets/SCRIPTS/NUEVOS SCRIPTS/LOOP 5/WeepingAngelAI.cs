using UnityEngine;
using UnityEngine.AI;

public class WeepingAngelAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;           // <<< DEBE SER LA MAINCAMERA
    public AudioSource staticSound;

    [Header("Configuración")]
    public float activationDelay = 0.5f;
    public float viewAngle = 45f;
    public float moveSpeed = 3f;

    public bool isActive { get; private set; } = false;

    private NavMeshAgent agent;
    private bool canMove = false;
    private float activationTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = moveSpeed;
        }

        // ---------------------------------------------
        // 🔥 TEST DE MOVIMIENTO (solo para debug)
        // Activalo si querés confirmar que el NavMeshAgent
        // está correctamente configurado.
        // ---------------------------------------------
#if UNITY_EDITOR
        // Descomentá estas líneas si querés probar el movimiento directo
        agent.speed = 3;
        agent.SetDestination(new Vector3(10, 0, 10));
        Debug.Log("TEST: Angel intentando moverse al punto (10,0,10)");
#endif
        // ---------------------------------------------
    }

    void Update()
    {
        if (!isActive) return;
        if (Time.time < activationTime) return;

        // --- DETECCIÓN DE MIRADA ---
        Vector3 dirFromPlayer = (transform.position - player.position).normalized;  
        Vector3 camForward = player.forward;

        float angle = Vector3.Angle(camForward, dirFromPlayer);
        bool playerIsLooking = angle < viewAngle;

        // --- COMPORTAMIENTO ---
        if (playerIsLooking)
        {
            canMove = false;

            if (agent != null)
                agent.ResetPath();

            if (staticSound && staticSound.isPlaying)
                staticSound.Stop();
        }
        else
        {
            canMove = true;

            if (staticSound && !staticSound.isPlaying)
                staticSound.Play();
        }

        // --- MOVIMIENTO (CON TUS LOGS) ---
        if (canMove && agent != null)
        {
            agent.SetDestination(player.position);
            Debug.Log("MOVIENDO: " + agent.destination);
        }
        else
        {
            Debug.Log("NO MOVIENDO (canMove=" + canMove + ")");
        }
    }

    public void ActivateAngel()
    {
        isActive = true;
        activationTime = Time.time + activationDelay;
    }

    public void DeactivateAngel()
    {
        isActive = false;
        canMove = false;

        if (staticSound && staticSound.isPlaying)
            staticSound.Stop();

        if (agent != null)
            agent.ResetPath();
    }
}

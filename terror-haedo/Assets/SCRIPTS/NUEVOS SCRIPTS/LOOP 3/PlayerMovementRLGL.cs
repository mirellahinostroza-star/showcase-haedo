using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementRLGL : MonoBehaviour
{
    [Header("Red Light, Green Light")]
    public RedLightGreenLightManager rlglManager;
    public Transform respawnPoint;
    public float moveThreshold = 0.01f;
    public float cameraThreshold = 0.5f; // sensibilidad de movimiento de cámara
    public float redLightDelay = 0.2f; // delay antes de morir en rojo

    [Header("Referencia a cámara")]
    public Transform playerCamera; // asignar la cámara del jugador aquí

    private CharacterController controller;
    private Vector3 lastPosition;
    private Vector2 lastCamEuler;
    private bool canDie = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = controller.transform.position;

        if (playerCamera != null)
            lastCamEuler = new Vector2(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y);

        if (rlglManager == null)
            Debug.LogWarning("⚠️ RedLightGreenLightManager no asignado al jugador.");
        if (respawnPoint == null)
            Debug.LogWarning("⚠️ RespawnPoint no asignado al jugador.");
    }

    void Update()
    {
        if (rlglManager == null || !rlglManager.enabled) return;

        bool isRedLight = rlglManager.isRedLight;

        // Delay antes de poder morir
        if (isRedLight && !canDie)
            StartCoroutine(EnableDeathAfterDelay());
        else if (!isRedLight)
            canDie = false;

        // Movimiento del jugador
        Vector3 moveDelta = controller.transform.position - lastPosition;
        float inputDelta = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));

        // Movimiento de la cámara
        bool camMoved = false;
        if (playerCamera != null)
        {
            Vector2 camEuler = new Vector2(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y);
            Vector2 camDelta = camEuler - lastCamEuler;

            if (Mathf.Abs(camDelta.x) > cameraThreshold || Mathf.Abs(camDelta.y) > cameraThreshold)
                camMoved = true;

            lastCamEuler = camEuler;
        }

        // Chequeo de muerte
        if (isRedLight && canDie && (moveDelta.magnitude > moveThreshold || inputDelta > 0f || camMoved))
        {
            Debug.Log("💀 Te moviste o giraste la cámara durante RED LIGHT!");
            DieAndRespawn();
        }

        lastPosition = controller.transform.position;
    }

    private IEnumerator EnableDeathAfterDelay()
    {
        yield return new WaitForSeconds(redLightDelay);
        canDie = true;
    }

    private void DieAndRespawn()
    {
        if (respawnPoint != null)
        {
            controller.enabled = false;
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            controller.enabled = true;
        }

        // Detener el minijuego al respawnear
        if (rlglManager != null)
            rlglManager.StopMinigame();

        canDie = false;
    }
}

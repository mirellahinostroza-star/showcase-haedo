using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;  
    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip[] footstepClips;

    [Header("Timers")]
    public float walkStepRate = 0.6f;
    public float runStepRate = 0.35f;

    private float stepTimer;

    void Update()
    {
        if (controller == null) return;

        bool isGrounded = controller.isGrounded;
        bool isMoving = controller.velocity.magnitude > 0.1f;

        if (isGrounded && isMoving)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            float rate = isRunning ? runStepRate : walkStepRate;

            stepTimer += Time.deltaTime;
            if (stepTimer >= rate)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; 
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}
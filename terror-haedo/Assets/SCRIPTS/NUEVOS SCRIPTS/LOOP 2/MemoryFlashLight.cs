using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]  
public class MemoryFlashLight : MonoBehaviour
{
    public MemoryFlashMiniGame miniGame;
    public float flashIntensity = 6f;

    [Header("Audio")]
    public AudioClip flashSound; 
    private AudioSource audioSource;

    private Light _light;
    private float baseIntensity;

    void Start()
    {
        _light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;   
        baseIntensity = _light.intensity;
    }

    public void Flash(float duration)
    {
   
        PlayFlashSound();

   
        StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float duration)
    {
        _light.intensity = flashIntensity;
        yield return new WaitForSeconds(duration);
        _light.intensity = baseIntensity;
    }

    void OnMouseDown()
    {
        if (miniGame != null)
        {
            Debug.Log($"[MemoryFlashLight] Click en {gameObject.name}");

       
            PlayFlashSound();

            miniGame.RegisterPlayerClick(this);
            Flash(0.25f); 
        }
    }

   
    private void PlayFlashSound()
    {
        if (flashSound != null && audioSource != null)
            audioSource.PlayOneShot(flashSound);
    }
}
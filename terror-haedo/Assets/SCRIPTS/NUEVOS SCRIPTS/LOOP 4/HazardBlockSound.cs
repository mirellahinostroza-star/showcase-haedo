using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HazardBlockSound : MonoBehaviour
{
    public Transform player;
    public float activationRadius = 6f;
    public float maxVolume = 1f;
    public float minVolume = 0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;        // sonido constante
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;        // empieza apagado

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationRadius)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            // Volumen proporcional a la distancia
            float vol = 1f - (distance / activationRadius);
            audioSource.volume = Mathf.Lerp(minVolume, maxVolume, vol);
        }
        else
        {
            // apagado suave
            if (audioSource.isPlaying)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 5f);

                if (audioSource.volume < 0.01f)
                    audioSource.Stop();
            }
        }
    }
}

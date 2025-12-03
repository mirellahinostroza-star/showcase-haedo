using UnityEngine;

public class StopSoundWhenKill : MonoBehaviour
{
    public string playerTag = "Player";
    public string soundTriggerName = "Loop1_StartTrigger";

    private AudioSource sonido;

    void Start()
    {
        // Buscar el trigger donde está el sonido
        GameObject trigger = GameObject.Find(soundTriggerName);

        if (trigger != null)
        {
            sonido = trigger.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("No se encontró el trigger con el nombre: " + soundTriggerName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag))
        {
            if (sonido != null && sonido.isPlaying)
                sonido.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (sonido != null && sonido.isPlaying)
                sonido.Stop();
        }
    }
}

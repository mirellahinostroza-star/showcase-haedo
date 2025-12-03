using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopSoundTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    public string triggerName = "Loop1_StartTrigger";

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && gameObject.name == triggerName)
        {
            audioSource.Play();
        }
    }
}

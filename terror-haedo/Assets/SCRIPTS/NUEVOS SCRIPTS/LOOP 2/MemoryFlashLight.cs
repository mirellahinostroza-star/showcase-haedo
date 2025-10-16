using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
[RequireComponent(typeof(Collider))]
public class MemoryFlashLight : MonoBehaviour
{
    public MemoryFlashMiniGame miniGame;
    public float flashIntensity = 6f;

    private Light _light;
    private float baseIntensity;

    void Start()
    {
        _light = GetComponent<Light>();
        baseIntensity = _light.intensity;
    }

    public void Flash(float duration)
    {
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
            miniGame.RegisterPlayerClick(this);
            Flash(0.25f);
        }
    }
}

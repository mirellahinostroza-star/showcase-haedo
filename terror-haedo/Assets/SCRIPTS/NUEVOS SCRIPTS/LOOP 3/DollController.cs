using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    public List<Light> greenLights;
    public List<Light> redLights;
    public Transform lookAtTarget; // el jugador o punto a mirar
    public float blinkDuration = 0.3f; // duración del parpadeo

    private bool isGreen = true;
    private Coroutine blinkCoroutine;

    public void SetLightState(bool green)
    {
        isGreen = green;

        Debug.Log($"{gameObject.name}: cambiando a {(green ? "🟢 verde" : "🔴 rojo")}");

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkLights(green));

        if (green)
            Debug.Log($"{gameObject.name}: Luces verdes activas, puede moverse ✅");
        else
            Debug.Log($"{gameObject.name}: Luces rojas activas, no puede moverse ❌");

        transform.rotation = Quaternion.Euler(0, green ? 180 : 0, 0);
    }

    private IEnumerator BlinkLights(bool green)
    {
        for (int i = 0; i < 2; i++)
        {
            SetLights(green, false);
            yield return new WaitForSeconds(blinkDuration / 2);
            SetLights(green, true);
            yield return new WaitForSeconds(blinkDuration / 2);
        }
        SetLights(green, true);
    }

    private void SetLights(bool green, bool state)
    {
        foreach (var l in greenLights) l.enabled = green ? state : !state;
        foreach (var l in redLights) l.enabled = green ? !state : state;
    }

    public bool IsLookingAtPlayer()
    {
        return !isGreen;
    }
}

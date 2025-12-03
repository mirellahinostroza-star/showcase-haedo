using UnityEngine;

public class CeilingTrap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform ceiling;         
    [SerializeField] private Transform startPosition;   
    [SerializeField] private Transform endPosition;     
    [SerializeField] private float normalSpeed = 1f;    
    [SerializeField] private float runningSpeed = 2f;   

    private bool isActive = false;

    public void StartMinigame()
    {
        ceiling.position = startPosition.position;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : normalSpeed;

        ceiling.position = Vector3.MoveTowards(
            ceiling.position,
            endPosition.position,
            currentSpeed * Time.deltaTime
        );
    }

    public void ResetCeiling()
    {
        ceiling.position = startPosition.position;
        isActive = false;
    }
}
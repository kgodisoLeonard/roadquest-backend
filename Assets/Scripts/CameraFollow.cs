using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your Car GameObject here in the Inspector
    public Vector3 offset = new Vector3(0f, 8f, -12f);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position behind the car
        Vector3 desiredPosition = target.position + offset;
        
        // Smoothly interpolate camera position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // Look slightly ahead of the car
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
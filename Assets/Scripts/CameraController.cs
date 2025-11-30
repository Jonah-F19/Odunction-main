using UnityEngine;
//test
public class CameraController : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float smoothSpeed = 6f; // Adjust for smooth movement
    public Vector3 offset; // Offset to keep the camera at a specific distance

    void LateUpdate()
    {
        if (player != null)
        {
            // Target position based on player's position + offset (for 2D, ignore Z-axis)
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            // Smoothly move the camera to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayController : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController
    public Image cutoutImage; // Reference to the UI Image

    void Start()
    {
        // Ensure the image starts hidden
        if (cutoutImage != null)
        {
            cutoutImage.enabled = false;
        }
    }

    void Update()
    {
        if (playerController != null && cutoutImage != null)
        {
            // Toggle image visibility based on hasCutout
            cutoutImage.enabled = playerController.hasCutout;
        }
    }
} 

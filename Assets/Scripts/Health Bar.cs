using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [Header("Health Bar Settings")]
    public Transform healthBar;      // Reference to the Health Bar Transform (The parent panel)
    public PlayerController playerController;  // Reference to the PlayerController script

    void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        if (healthBar == null)
        {
            Debug.LogError("HealthBar Transform is not assigned.");
        }
    }

    void Update()
    {
        if (playerController != null && healthBar != null)
        {
            UpdateHealthBarScale();
        }
    }

    // Update the health bar's scale based on the player's current health
    private void UpdateHealthBarScale()
    {
        // Calculate health as a percentage (0 to 1)
        float healthPercent = Mathf.Clamp((float)playerController.health / 100f, 0f, 1f);

        // Update the X scale of the health bar based on health percentage
        healthBar.localScale = new Vector3(healthPercent, 1f, 1f);  // Only scale X axis
    }
}

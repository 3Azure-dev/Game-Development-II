using UnityEngine;
using UnityEngine.UI;  // Required for Slider

public class HealthBarFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform targetTransform;       // The dragon/enemy this health bar follows
    public Vector3 offset = new Vector3(0, 1.5f, 0);  // Position above the enemy

    [Header("Health Bar")]
    public Slider healthSlider;             // The UI Slider showing health

    private int maxHealth;                  // Cache max health
    private Player playerRef;               // Reference to the player (for health reading)

    void Start()
    {
        // If no target assigned, assume this health bar is parented under the enemy
        if (targetTransform == null && transform.parent != null)
            targetTransform = transform.parent;

        // Find player to read health
        playerRef = FindAnyObjectByType<Player>();

        if (playerRef != null)
        {
            maxHealth = playerRef.maxHealth;
        }

        // Initialize slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
    {
        // 1. Follow the target position
        if (targetTransform != null)
        {
            transform.position = targetTransform.position + offset;
        }

        // 2. Update the health slider fill
        if (healthSlider != null && playerRef != null)
        {
            healthSlider.value = playerRef.currentHealth;
        }
    }
}

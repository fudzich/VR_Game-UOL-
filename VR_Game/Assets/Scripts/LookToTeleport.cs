using UnityEngine;

public class LookToTeleport : MonoBehaviour
{
    public Raycaster raycaster; // Reference to the Raycaster script
    public float teleportDelay = 2f; // Time in seconds to look before teleporting
    public Transform playerTransform; // Player's transform, to teleport
    private float gazeTimer = 0f;

    private OutlineOnLook currentPlatform = null;
    
    private Vector3 targetPosition;

    void Update()
    {

        if (raycaster.outline != null)
        {
            // Player is looking at a platform
            if (raycaster.outline != currentPlatform)
            {
                // New platform looked at, reset timer
                currentPlatform = raycaster.outline;
                gazeTimer = 0f;
            }
            else
            {
                // Continuing to look at the same platform
                gazeTimer += Time.deltaTime;

                // Calculate remaining time
                float timeLeft = Mathf.Max(0f, teleportDelay - gazeTimer);

                // Log the time left
                Debug.Log($"Time left to teleport: {timeLeft:F2} seconds");

                if (gazeTimer >= teleportDelay)
                {
                    // Teleport the player
                    TeleportPlayer(currentPlatform.transform);
                    // Reset timer
                    gazeTimer = 0f;
                }
            }
        }
        else
        {
            // Not looking at any platform
            currentPlatform = null;
            gazeTimer = 0f;
        }
    }

    void TeleportPlayer(Transform targetTransform)
    {
        if (playerTransform != null && targetTransform != null)
        {
            Vector3 targetPosition = targetTransform.position;
            // Teleport the player
            playerTransform.position = targetPosition;

            Debug.Log("Player teleported to platform.");
        }
    }
}
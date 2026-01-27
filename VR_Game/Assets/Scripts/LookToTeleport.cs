using UnityEngine;

public class LookToTeleport : MonoBehaviour
{
    public Raycaster raycaster; // Reference to your Raycaster script
    public float teleportDelay = 2f; // Time in seconds to look before teleporting
    public Transform playerTransform; // Player's transform, to teleport
    private float gazeTimer = 0f;

    private OutlineOnLook currentPlatform = null;
    
    public float moveSpeed = 2f; // Speed of smooth movement
    private bool isTeleporting = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (isTeleporting)
        {
            // Move player towards target position smoothly
            float step = moveSpeed * Time.deltaTime;
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, step);

            // Check if close enough to stop
            if (Vector3.Distance(playerTransform.position, targetPosition) < 0.01f)
            {
                isTeleporting = false;
                Debug.Log("Teleportation complete.");
            }
            return;
        }

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
                    //TeleportPlayer(currentPlatform.transform);
                    
                    // Reset timer to prevent repeated teleportation
                    gazeTimer = 0f;

                    // Start smooth teleport
                    targetPosition = currentPlatform.transform.position;
                    isTeleporting = true;
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
            // Optional: Adjust position to avoid clipping or positioning at a good spot
            Vector3 targetPosition = targetTransform.position;

            // Teleport the player
            playerTransform.position = targetPosition;

            Debug.Log("Player teleported to platform.");
        }
    }
}
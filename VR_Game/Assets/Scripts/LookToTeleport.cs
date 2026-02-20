using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LookToTeleport : MonoBehaviour
{
    public Raycaster raycaster; // Reference to the Raycaster script
    public float teleportDelay = 2f; // Time in seconds to look before teleporting
    public Transform playerTransform; // Player's transform, to teleport
    private float gazeTimer = 0f;

    public Image fadeImage; // UI black Image that is used to blacken screen dring teleportation
    public float fadeDuration = 1f; // Duration of fade in/out

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
                    //Fade screen to black
                    StartCoroutine(Fade(0f, 1f, fadeDuration));
                    // Teleport the player
                    TeleportPlayer(currentPlatform.transform);
                    //Wait for a moment
                    StartCoroutine(WaitBlack(0.3f));
                    //Unfade screen from black
                    StartCoroutine(Fade(1f, 0f, fadeDuration));
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

    // Wait for a time
    public IEnumerator WaitBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    // Change transparency of the image over a time
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure final alpha is set
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
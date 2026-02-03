using UnityEngine;
using UnityEngine.SceneManagement;

public class LookToUseButtons : MonoBehaviour
{
    public Raycaster raycaster; // Reference to your Raycaster script
    public float activationDelay = 2f; // Time in seconds to look
    private float gazeTimer = 0f;

    private OutlineOnLook currentPlatform = null;

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
                float timeLeft = Mathf.Max(0f, activationDelay - gazeTimer);

                if (gazeTimer >= activationDelay)
                {
                    // Activate Platform
                    ActivateButtons(currentPlatform.gameObject);
                    
                    // Reset timer to prevent repeated teleportation
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

    void ActivateButtons(GameObject button)
    {
        if(button.name == "Start Game"){
             SceneManager.LoadScene("SampleScene");
        }

        if(button.name == "Exit"){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            // Quit the application
            Application.Quit();
            #endif
        }

        if(button.name == "Return To Menu"){
            SceneManager.LoadScene("MainMenu");
        }
    }
}

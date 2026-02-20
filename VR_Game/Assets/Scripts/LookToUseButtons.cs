using UnityEngine;
using UnityEngine.SceneManagement;

public class LookToUseButtons : MonoBehaviour
{
    public Raycaster raycaster; // Reference to the Raycaster script
    public float activationDelay = 2f; // Time in seconds to look
    private float gazeTimer = 0f;

    AudioManager audioManager;

    private OutlineOnLook currentPlatform = null;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (raycaster.outline != null)
        {
            // Player is looking at a button
            if (raycaster.outline != currentPlatform)
            {
                // New platformbutton looked at, reset timer
                currentPlatform = raycaster.outline;
                gazeTimer = 0f;
            }
            else
            {
                // Continuing to look at the same button
                gazeTimer += Time.deltaTime;

                // Calculate remaining time
                float timeLeft = Mathf.Max(0f, activationDelay - gazeTimer);

                if (gazeTimer >= activationDelay)
                {
                    // Activate Button
                    ActivateButtons(currentPlatform.gameObject);
                    // Reset timer
                    gazeTimer = 0f;
                }
            }
        }
        else
        {
            // Not looking at any button
            currentPlatform = null;
            gazeTimer = 0f;
        }
    }

    void ActivateButtons(GameObject button)
    {
        audioManager.PlaySFX(audioManager.buttonClick);

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
            DataHolder.points = 0;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

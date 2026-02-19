using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            SceneManager.LoadScene("EndMenu");
        }
    }
}

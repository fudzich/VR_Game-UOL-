using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            DataHolder.points++;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign your prefab in the inspector
    public float minInterval = 1f;   // Minimum spawn interval
    public float maxInterval = 5f;   // Maximum spawn interval
    public int maxBalls = 3;         // Maximum number of balls allowed
    public float forceAlongX = -500f; // Force magnitude applied

    private float spawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Generate a random spawn interval between min and max
        spawnInterval = Random.Range(minInterval, maxInterval);
        // Start the spawning coroutine
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
           
            // Check current number of balls
            GameObject[] existingBalls = GameObject.FindGameObjectsWithTag("Ball");
            if (existingBalls.Length < maxBalls)
            {
                SpawnPrefab();
            }
            
            // Generate a new random interval for the next spawn
            spawnInterval = Random.Range(minInterval, maxInterval);
        }
    }

    // Call this method to spawn the prefab
    public void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            GameObject newBall = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(new Vector3(forceAlongX, 0, 0));
            }
            else
            {
                Debug.LogWarning("Spawned object does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Prefab to spawn is not assigned.");
        }
    }
}

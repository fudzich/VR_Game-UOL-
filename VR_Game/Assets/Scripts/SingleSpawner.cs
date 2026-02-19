using UnityEngine;
using System.Collections;

public class SingleSpawner : MonoBehaviour
{

    public GameObject prefabToSpawn;
    public float forceAlongX = -500f;
    
    void Start()
    {
        SpawnPrefab();
    }

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

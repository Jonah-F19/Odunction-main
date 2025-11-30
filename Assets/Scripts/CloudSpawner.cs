using UnityEngine;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab; // Assign your cloud prefab in the inspector
    public int cloudCount = 10; // Number of clouds to spawn

    public float minX = -10f; // Left boundary of the spawn area
    public float maxX = 10f; // Right boundary of the spawn area
    public float minY = 5f; // Minimum height of clouds
    public float maxY = 10f; // Maximum height of clouds

    public float minDistanceBetweenClouds = 2f; // Minimum distance to prevent overlap

    private List<Vector3> cloudPositions = new List<Vector3>();

    void Start()
    {
        if (CanFitClouds())
        {
            SpawnClouds(); // Spawn a limited number of clouds
        }
        else
        {
            Debug.LogError("Cannot fit the requested number of clouds in the given area with the minimum distance.");
        }
    }

    bool CanFitClouds()
    {
        float areaWidth = maxX - minX;
        float areaHeight = maxY - minY;
        float availableArea = areaWidth * areaHeight;
        float requiredArea = cloudCount * (Mathf.PI * Mathf.Pow(minDistanceBetweenClouds / 2, 2));

        return availableArea >= requiredArea;
    }

    void SpawnClouds()
    {
        if (cloudPrefab != null)
        {
            int spawnedClouds = 0;
            int maxAttempts = cloudCount * 10; // Avoid infinite loops
            int attempts = 0;

            while (spawnedClouds < cloudCount && attempts < maxAttempts)
            {
                Vector3 spawnPosition = GetRandomPosition();
                attempts++;

                // Check if the new position is far enough from existing clouds
                if (IsPositionValid(spawnPosition))
                {
                    Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
                    cloudPositions.Add(spawnPosition);
                    spawnedClouds++;
                }
            }

            if (spawnedClouds < cloudCount)
            {
                Debug.LogWarning("Could not spawn all clouds due to space constraints.");
            }
        }
        else
        {
            Debug.LogWarning("Cloud prefab not assigned!");
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0);
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (var existingPosition in cloudPositions)
        {
            if (Vector3.Distance(existingPosition, position) < minDistanceBetweenClouds)
            {
                return false; // Too close to an existing cloud
            }
        }
        return true;
    }
}

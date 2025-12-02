using UnityEngine;
using System;
using JetBrains.Annotations;

public class SkeletonManager : MonoBehaviour
{
    public GameObject RegularSkeletonPrefab;
    public int skeletonCount = 0;

    void Start()
    {
        skeletonCount = 0;
    }

    // circle radius around player to spawn skeletons
    public void SpawnSkeletonsAroundPlayer(Transform playerTransform, int count)
    {
        float spawnRadius = 2.0f; // radius around player to spawn skeletons

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 spawnPosition = new Vector3(
                playerTransform.position.x + Mathf.Cos(angle) * spawnRadius,
                playerTransform.position.y + Mathf.Sin(angle) * spawnRadius,
                0f
            );

            Instantiate(RegularSkeletonPrefab, spawnPosition, Quaternion.identity);
            skeletonCount++;
        }
    }

}

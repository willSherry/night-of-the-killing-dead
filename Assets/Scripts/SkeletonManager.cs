using System.Net.NetworkInformation;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public static SkeletonManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        numberOfSkeletons = 0;
    }

    public GameObject basicSkeleton;
    public float spawnRadius = 2.5f;

    public float skeletonDistance = 5f;
    public float enemyDetectionRadius = 10f;
    public int numberOfSkeletons;

    public void SpawnSkeleton(GameObject skeleton, Vector3 position)
    {
        Instantiate(skeleton, position, Quaternion.identity);
        Debug.Log("Skeleton spawned at position: " + position);
    }

}
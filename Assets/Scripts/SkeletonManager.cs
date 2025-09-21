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
    }

    public GameObject basicSkeleton;
    public float spawnRadius = 1f;

    public void SpawnSkeleton(GameObject skeleton, Vector3 position)
    {
        Instantiate(skeleton, position, Quaternion.identity);
        Debug.Log("Skeleton spawned at position: " + position);
    }

}
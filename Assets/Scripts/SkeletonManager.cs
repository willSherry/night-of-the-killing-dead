using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public GameObject basicSkeleton;

    public void SpawnSkeleton(GameObject skeleton, Vector3 position)
    {
        // spawn SFX, VFX, etc. can be added here
        Instantiate(skeleton, position, Quaternion.identity);
    }


}
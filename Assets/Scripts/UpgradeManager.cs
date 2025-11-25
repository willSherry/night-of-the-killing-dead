using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public SkeletonManager skeletonManager;
    void Start()
    {
        skeletonManager = GetComponent<SkeletonManager>();
    }
    public void IncreaseSkeletonCount(int amount)
    {
        if (skeletonManager != null)
        {
            skeletonManager.skeletonCount += amount;
            Debug.Log("Increased skeleton count to " + skeletonManager.skeletonCount);
        }
        else
        {
            Debug.LogWarning("SkeletonManager not found in the scene.");
        }
    }

    void resetSkeletonCount()
    {
        skeletonManager.skeletonCount = 0;
    }
}

using UnityEngine;

public enum SkeletonType
{
    Regular,
    Archer,
    Warrior
}

public enum SkeletonAction
{
    Idle,
    Following,
    Attacking,
    Dying
}

public class Skeleton : MonoBehaviour
{
    public void Resurrect()
    {
        // resurrect vfx and sfx
    }

    public void ApplyBehavior(SkeletonAction currentAction)
    {
        switch (currentAction)
        {
            case SkeletonAction.Idle:
                // idle behavior
                break;
            case SkeletonAction.Following:
                // following behavior
                break;
            case SkeletonAction.Attacking:
                // attacking behavior
                break;
            case SkeletonAction.Dying:
                // dying behavior
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        // idle behavior implementation
    }
    void Follow()
    {
        // follow behavior implementation
    }
    void Attack()
    {
        // attack behavior implementation
    }
    void Die()
    {
        // die behavior implementation
    }
}
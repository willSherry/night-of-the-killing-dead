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

}

using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    private SkeletonManager skeletonManager;
    private float fieldOfView;
    private int previousSkeletonCount = 0;

    void Start()
    {
        mainCamera = Camera.main;
        skeletonManager = SkeletonManager.instance;
        fieldOfView = 80f;
        previousSkeletonCount = skeletonManager.numberOfSkeletons;
    }

    void Update()
    {
        int numberOfSkeletons = skeletonManager.numberOfSkeletons;

        if (numberOfSkeletons > previousSkeletonCount)
        {
            fieldOfView += 0.25f * (numberOfSkeletons - previousSkeletonCount);
        }

        if (fieldOfView < 80f)
        {
            fieldOfView = 80f;
        }

        mainCamera.fieldOfView = fieldOfView;
        previousSkeletonCount = numberOfSkeletons;
    }
}
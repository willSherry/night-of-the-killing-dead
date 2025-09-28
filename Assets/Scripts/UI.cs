using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public Text numberOfSkeletonsText;
    private SkeletonManager skeletonManager;

    void Start()
    {
        skeletonManager = SkeletonManager.instance;
        numberOfSkeletonsText.text = "Number of skeletons: 0";
    }

    void Update()
    {
        int numberOfSkeletons = skeletonManager.numberOfSkeletons;
        numberOfSkeletonsText.text = "Skeletons: " + numberOfSkeletons.ToString();
    }
}
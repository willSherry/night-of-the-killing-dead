using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{   
    [SerializeField] GameObject levelUpPanel;
    void Start()
    {
        levelUpPanel.SetActive(false);
    }

    public void ShowLevelUpPanel()
    {
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);
    }

    public void HideLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }

    void addMoreSkeletons()
    {
        
    }
}

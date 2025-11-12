using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowLevelUpPanel()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void HideLevelUpPanel()
    {
        gameObject.SetActive(false);
    }
}

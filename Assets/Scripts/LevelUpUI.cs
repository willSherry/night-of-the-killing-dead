using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{   
    [SerializeField] GameObject levelUpPanel;
    public UpgradeManager upgradeManager;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public TextMeshProUGUI upgradeText1;   
    public TextMeshProUGUI upgradeText2;
    public TextMeshProUGUI upgradeText3;
    public Image upgradeImage1;
    public Image upgradeImage2;
    public Image upgradeImage3;
    void Start()
    {
        upgradeManager = GetComponent<UpgradeManager>();
        levelUpPanel.SetActive(false);
        upgradeButton1.onClick.AddListener(() => {
            addMoreSkeletons();
            CloseLevelUpPanel();
        });
    }

    public void ShowLevelUpPanel()
    {
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);
    }

    public void CloseLevelUpPanel()
    {
        Time.timeScale = 1f;
        levelUpPanel.SetActive(false);
    }

    void addMoreSkeletons()
    {
        if (upgradeManager != null)
        {
            upgradeManager.IncreaseSkeletonCount(5);
            CloseLevelUpPanel();
        }
        else
        {
            Debug.LogWarning("UpgradeManager not found in the scene.");
        }
    }

}

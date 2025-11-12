using UnityEngine;
using System;
using JetBrains.Annotations;

public class XPManager : MonoBehaviour
{
    public int CurrentLevel;
    public int CurrentXP;
    public int XPToNextLevel;
    public int XPMultiplier = 1;
    public LevelUpUI levelUpScreen;
    void Start()
    {
        CurrentLevel = 1;
        CurrentXP = 0;
        XPToNextLevel = 10;
    }

    void LevelUp()
    {
        CurrentLevel++;
        CurrentXP = 0;
        XPToNextLevel = Mathf.RoundToInt(XPToNextLevel * 1.5f);
        Debug.Log("Leveled up to " + CurrentLevel);
        levelUpScreen.ShowLevelUpPanel();
    }
    
    public void AddXP(int amount)
    {
        Debug.Log("Gained " + (amount * XPMultiplier) + " XP");
        Debug.Log($"Current XP: {CurrentXP} / {XPToNextLevel}");
        CurrentXP += (amount * XPMultiplier);
        if (CurrentXP >= XPToNextLevel)
        {
            LevelUp();
        }
    }
}

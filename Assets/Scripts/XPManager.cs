using UnityEngine;
using System;
using JetBrains.Annotations;

public class XPManager : MonoBehaviour
{
    public int CurrentLevel;
    public int CurrentXP;
    public int XPToNextLevel;
    public int XPMultiplier = 1;
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
    }
    
    public void AddXP(int amount)
    {
        CurrentXP += (amount * XPMultiplier);
        if (CurrentXP >= XPToNextLevel)
        {
            LevelUp();
        }
    }
}

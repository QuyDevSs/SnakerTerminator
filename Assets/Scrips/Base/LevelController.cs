using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelController : ProcessingController
{
    public Action<int> onLevelUp;

    int level = 1;

    [SerializeField]
    TextMeshPro txtLevel;

    public int Level
    {
        set
        {
            this.level = value;
            txtLevel.text = this.level.ToString();
            if (onLevelUp != null)
            {
                onLevelUp(level);
            }
        }
        get
        {
            return level;
        }
    }
    protected override void OnChangeCurrentValue(float value)
    {
        if (value >= MaxValue)
        {
            CurrentValue = value % MaxValue;
            Level += (int)(value / MaxValue);
        }
    }
}

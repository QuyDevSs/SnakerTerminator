using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HPController : ProcessingController
{
    public float HP
    {
        set
        {
            float difference = value - MaxValue;
            MaxValue = value;
            CurrentValue += difference;
        }
        get
        {
            return HP;
        }
    }
    public Action onDie;

    public void TakeDamage(float damage)
    {
        CurrentValue -= damage;
    }

    protected override void OnChangeCurrentValue(float value)
    {
        if (value == 0)
        {
            if (onDie != null)
            {
                onDie();
            }
        }
        if (value > MaxValue)
        {
            CurrentValue = MaxValue;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProcessingController : MonoBehaviour
{
    float currentValue = 0;

    float maxValue = 1;
    public float MaxValue
    {
        set
        {
            maxValue = value;
            if (maxValue <= 0)
            {
                maxValue = 1f;
            }
        }
        get
        {
            return maxValue;
        }
    }
    public float CurrentValue
    {
        set
        {
            currentValue = value;
            if (value < 0)
            {
                currentValue = 0;
            }
            OnChangeCurrentValue(currentValue);
            DisplayValue();
        }
        get
        {
            return currentValue;
        }
    }

    protected abstract void OnChangeCurrentValue(float value);

    protected virtual void DisplayValue()
    {
        transform.localScale = new Vector3((float)(currentValue / maxValue), transform.localScale.y, transform.localScale.z);
    }
}

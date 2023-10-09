using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowInfo
{
    public int rate;
    public float time;
}
public class SlowEffect : MonoBehaviour, ItemEffect
{
    public SlowInfo info;

    public object Info { set => info = (SlowInfo)value; }

    public void Active()
    {
        EntityController entityController = GetComponentInChildren<EntityController>();
        entityController.speed *= (1 - info.rate);
        //StartCoroutine(dot());
    }
    
}

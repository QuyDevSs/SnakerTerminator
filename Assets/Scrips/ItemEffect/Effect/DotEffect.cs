using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotInfo
{
    public int hp;
    public float time;
    public float unitTime;
}

public class DotEffect : MonoBehaviour, ItemEffect
{
    public DotInfo info;

    public object Info { set => info = (DotInfo)value; }

    public void Active()
    {
        StartCoroutine(dot());
    }
    IEnumerator dot()
    {
        while (info.time > 0)
        {
            yield return new WaitForSeconds(info.unitTime);
            HPController hPController = GetComponentInChildren<HPController>();
            hPController.CurrentValue += info.hp;
            info.time -= info.unitTime;
        }
        if (info.time <= 0)
        {
            Destroy(this);
        }
    }
}

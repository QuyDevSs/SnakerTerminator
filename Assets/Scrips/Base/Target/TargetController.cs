using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    static List<TargetController> targets = new List<TargetController>();
    public static GameObject GetTarget(FilterTargetController filter)
    {
        GameObject bestTarget = null;

        foreach (TargetController target in targets)
        {
            if (!filter.CheckTarget(target.gameObject)) continue;
            if (bestTarget == null)
            {
                bestTarget = target.gameObject;
                continue;
            }
            float distanceCurrentTarget = Vector3.Distance(filter.transform.position, bestTarget.transform.position);
            float distanceTarget = Vector3.Distance(filter.transform.position, target.transform.position);
            if (distanceCurrentTarget > distanceTarget)
            {
                bestTarget = target.gameObject;
            }
        }
        return bestTarget;
    }

    private void Awake()
    {
        if (!targets.Contains(this))
            targets.Add(this);
    }
    private void OnEnable()
    {
        if (!targets.Contains(this))
            targets.Add(this);
    }

    private void OnDestroy()
    {
        if (targets.Contains(this))
            targets.Remove(this);
    }
    private void OnDisable()
    {
        if (targets.Contains(this))
            targets.Remove(this);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetDistance : MonoBehaviour, ICheckTarget
{
    public float range;
    public bool CheckTarget(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= range) return true;
        return false;
    }
}
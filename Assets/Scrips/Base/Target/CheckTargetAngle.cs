using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetAngle : MonoBehaviour, ICheckTarget
{
    public float range;
    public Transform body;
    public bool CheckTarget(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Vector3.Angle(dir, body.up);

        if (angle <= range/2) return true;
        return false;
    }
}
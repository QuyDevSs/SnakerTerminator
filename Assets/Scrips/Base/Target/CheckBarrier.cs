using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBarrier : MonoBehaviour, ICheckTarget
{
    public bool CheckTarget(GameObject target)
    {
        LayerMask barrierLayerMask = LayerMask.GetMask("Barrier");
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, barrierLayerMask);
        if (hit.transform == null)
        {
            return true;
        }
        return false;
    }
}

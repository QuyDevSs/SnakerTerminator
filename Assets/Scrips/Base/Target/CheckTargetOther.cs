using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetOther : MonoBehaviour, ICheckTarget
{
    public List<GameObject> oldTargets;
    public bool CheckTarget(GameObject target)
    {
        if (!oldTargets.Contains(target))
        {
            return true;
        }
        return false;
    }
}

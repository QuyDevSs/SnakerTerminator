using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZigZag : MonoBehaviour, ITypeMove
{
    Vector3 direction = Vector3.zero;
    int rightDrection = 1;
    float factor = 1;
    [SerializeField]
    float range;
    
    private void Start()
    {
    }
    private void OnEnable()
    {
        factor = -range;
        direction = Vector3.zero;
        rightDrection = 1;
    }
    public Vector3 Move()
    {
        return direction;
    }
    void Update()
    {
        if (Mathf.Abs(factor) >= range)
        {
            rightDrection = -rightDrection;
        }
        factor += rightDrection;
        factor = Mathf.Clamp(factor, -range, range);
        direction = factor * transform.right;
    }
}

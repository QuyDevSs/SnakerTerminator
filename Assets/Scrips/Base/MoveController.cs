using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed;
    public virtual void Move(Vector3 direction)
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
    public virtual void MoveTo(Transform _transform)
    {
        Vector3 direction = (_transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}

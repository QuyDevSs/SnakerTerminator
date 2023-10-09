using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    void Update()
    {
        Vector2 normal = GetBarrierNormal();
        Vector2 reflectedDirection = Vector2.Reflect(transform.up, normal);
        transform.up = reflectedDirection.normalized;
    }
    Vector2 GetBarrierNormal()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float distance = spriteRenderer.bounds.size.y;
        LayerMask barrierLayerMask = LayerMask.GetMask("Barrier");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, barrierLayerMask);
        if (hit.collider != null)
        {
            return hit.normal;
        }
        return Vector2.zero;
    }
}

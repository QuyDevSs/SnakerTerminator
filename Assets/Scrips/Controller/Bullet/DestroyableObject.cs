using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class DestroyableObject : MonoBehaviour
{
    void Update()
    {
        if (IsBarrier())
        {
            Destroy(gameObject);
        }
    }
    bool IsBarrier()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float distance = spriteRenderer.bounds.size.y;
        LayerMask barrierLayerMask = LayerMask.GetMask("Barrier");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, barrierLayerMask);
        if (hit.collider == null)
        {
            return false;
        }
        return true;
    }
}

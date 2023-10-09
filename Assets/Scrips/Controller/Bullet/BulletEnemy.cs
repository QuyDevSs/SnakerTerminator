using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BulletController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        bulletTypes = BulletTypes.Normal;
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float distance = spriteRenderer.bounds.size.y;
        LayerMask playerLayerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, playerLayerMask);

        if (hit.transform != null)
        {
            IHit iHit = hit.transform.GetComponentInParent<IHit>();
            if (iHit != null)
            {
                iHit.OnHit(Damage, this);
                this.EndBullet();
                return;
            }
        }
        Move(transform.up);
    }
}

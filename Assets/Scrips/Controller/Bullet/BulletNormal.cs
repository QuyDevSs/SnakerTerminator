using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trúng quái destroy, tìm mục tiêu
public class BulletNormal : BulletController
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
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, enemyLayerMask);
        Debug.DrawLine(transform.position, transform.position + transform.up * distance, Color.red);

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
        
        if (target != null && target.gameObject.activeSelf)
        {
            transform.up = target.transform.position - transform.position;
            MoveTo(target.transform);
        }
        else
        {
            target = null;
            Move(transform.up);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trúng quái destroy, tìm mục tiêu
public class BulletNormal : BulletController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        lifeTime = 3f;
        bulletTypes = BulletTypes.Normal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            IHit iHit = collision.transform.GetComponentInParent<IHit>();
            if (iHit != null)
            {
                iHit.OnHit(Damage, this);
                EndBullet();
            }
        }
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            EndBullet();
        }
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //float distance = spriteRenderer.bounds.size.y;
        //LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, enemyLayerMask);

        //if (hit.transform != null)
        //{
        //    IHit iHit = hit.transform.GetComponentInParent<IHit>();
        //    if (iHit != null)
        //    {
        //        iHit.OnHit(Damage, this);
        //        this.EndBullet();
        //        return;
        //    }
        //}
        
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

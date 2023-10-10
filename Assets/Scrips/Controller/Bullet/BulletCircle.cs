using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : BulletController
{
    protected override void OnEnable()
    {
        lifeTime = 0.5f;
        bulletTypes = BulletTypes.Circle;
        base.OnEnable();
    }
    private void Update()
    {
        if (isPause)
        {
            return;
        }
        if (lifeTime <= 0)
        {
            EndBullet();
        }
        lifeTime -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IHit iHit = collision.transform.GetComponentInParent<IHit>();
            if (iHit != null)
            {
                iHit.OnHit(Damage, this);
                return;
            }
        }
    }
    public override void SetUp()
    {
    }
}

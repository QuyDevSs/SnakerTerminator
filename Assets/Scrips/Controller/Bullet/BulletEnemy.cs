using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BulletController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        lifeTime = 3f;
        bulletTypes = BulletTypes.Enemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayerMask = LayerMask.NameToLayer("Player");
        if (collision.gameObject.layer == playerLayerMask)
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
        if (lifeTime <= 0)
        {
            EndBullet();
        }
        lifeTime -= Time.deltaTime;
        Move(transform.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlants : BulletController
{
    BouncingScreen bouncingScreen;
    BouncingObject bouncingObject;
    protected override void OnEnable()
    {
        bulletTypes = BulletTypes.Plants;
        base.OnEnable();
        bouncingScreen = GetComponent<BouncingScreen>();
        bouncingObject = GetComponent<BouncingObject>();
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
                if (Level <= 1)
                {
                    EndBullet();
                }
            }
        }
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        Move(transform.up);
    }
    public override void SetUp()
    {
        if (Level == 3)
        {
            bouncingScreen.enabled = true;
            bouncingObject.enabled = true;
        }
        else
        {
            bouncingScreen.enabled = false;
            bouncingObject.enabled = false;
        }
    }
}

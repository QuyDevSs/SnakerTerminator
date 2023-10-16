using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartFireController : PartController
{
    FilterTargetController filterTarget;
    public override void OnEnable()
    {
        base.OnEnable();
        bodyTypes = BodyTypes.Fire;
        filterTarget = GetComponent<FilterTargetController>();
        DamageMultiplier = Constants.FIRE_BODY_DAMAGE_MULTIPLIER;
        Level = 0;
    }
    public override void Update()
    {
        if (isPause)
        {
            return;
        }
        Shoot();
    }
    public override void Shoot()
    {
        if (lastShootTime <= Time.time)
        {
            GameObject target = TargetController.GetTarget(filterTarget);
            if (target == null)
            {
                return;
            }
            base.Shoot();
            //Sound.Instance.PlaySound("laser_shot");
            RotateGun(target.transform.position - transform.position);
            BulletController bullet = Create.Instance.CreateBulletFire(tranShoot.position, tranShoot.rotation);
            bullet.Damage = (int)(Damage * DamageMultiplier);
            bullet.target = target;
            bullet.Level = Level;
            bullet.parent = gameObject;
            bullet.SetUp();
            lastShootTime = Time.time + 1 / Asdp;
        }
    }
    public override void Upgrade(int nextLevel)
    {
        switch (nextLevel)
        {
            case 1:
                Level = 1;
                DamageMultiplier = Constants.FIRE_BODY_DAMAGE_MULTIPLIER_LEVEL1;
                break;
            case 2:
                Level = 2;
                DamageMultiplier = Constants.FIRE_BODY_DAMAGE_MULTIPLIER_LEVEL2;
                break;
            case 3:
                Level = 3;
                DamageMultiplier = Constants.FIRE_BODY_DAMAGE_MULTIPLIER_LEVEL3;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartNormalController : PartController
{
    FilterTargetController filterTarget;
    public override void OnEnable()
    {
        base.OnEnable();
        bodyTypes = BodyTypes.Normal;
        filterTarget = GetComponent<FilterTargetController>();
        DamageMultiplier = Constants.NORMAL_BODY_DAMAGE_MULTIPLIER;
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
            Sound.Instance.PlaySound("laser_shot");
            RotateGun(target.transform.position - transform.position);
            BulletController bullet = Create.Instance.CreateBulletNormal(tranShoot);
            bullet.Damage = Damage * Constants.NORMAL_BODY_DAMAGE_MULTIPLIER;
            lastShootTime = Time.time + 1 / Asdp;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretController : PartController
{
    FilterTargetController filterTarget;
    public override void OnEnable()
    {
        base.OnEnable();
        bodyTypes = BodyTypes.GunTurret;
        filterTarget = GetComponent<FilterTargetController>();
    }
    public override void Update()
    {
        if (isPause)
        {
            return;
        }
        if (lastShootTime <= Time.time)
        {
            Shoot();
            lastShootTime = Time.time + 1 / Asdp;
        }
        Move();
    }
    public override void Shoot()
    {
        GameObject target = TargetController.GetTarget(filterTarget);
        if (target == null)
        {
            return;
        }
        base.Shoot();
        RotateGun(target.transform.position - transform.position);
        BulletController bullet = Create.Instance.CreateBulletNormal(tranShoot);
        bullet.Damage = Damage * Constants.NORMAL_BODY_DAMAGE_MULTIPLIER;
    }
    public void Move()
    {
        transform.position += transform.up * 0.001f;
    }
}

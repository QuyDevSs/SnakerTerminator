using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLightController : PartController
{
    FilterTargetController filterTarget;
    GameObject target;
    public bool isShoot = true;
    public override void OnEnable()
    {
        base.OnEnable();
        bodyTypes = BodyTypes.Light;
        filterTarget = GetComponent<FilterTargetController>();
        target = null;
        DamageMultiplier = Constants.ELECTRIC_BODY_DAMAGE_MULTIPLIER;
        Level = 0;
    }
    public override void Update()
    {
        if (isPause)
        {
            return;
        }
        if (target != null)
        {
            RotateGun(target.transform.position - transform.position);
        }
        Shoot();
    }
    public override void Shoot()
    {
        if (isShoot)
        {
            target = TargetController.GetTarget(filterTarget);
            if (target == null)
            {
                return;
            }
            base.Shoot();
            isShoot = false;
            BulletLight bullet = (BulletLight)Create.Instance.CreateBulletLight();
            bullet.Damage = Damage * Constants.ELECTRIC_BODY_DAMAGE_MULTIPLIER;
            bullet.speed = Asdp;
            bullet.target = target;
            bullet.partLight = this;
            bullet.Level = Level;
            bullet.SetUp();
        }
    }
    public override void Upgrade(int nextLevel)
    {
        switch (nextLevel)
        {
            case 1:
                Level = 1;
                DamageMultiplier = Constants.ELECTRIC_BODY_DAMAGE_MULTIPLIER_LEVEL1;
                break;
            case 2:
                Level = 2;
                DamageMultiplier = Constants.ELECTRIC_BODY_DAMAGE_MULTIPLIER_LEVEL2;
                break;
            case 3:
                Level = 3;
                DamageMultiplier = Constants.ELECTRIC_BODY_DAMAGE_MULTIPLIER_LEVEL3;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPlantsController : PartController
{
    FilterTargetController filterTarget;
    public override void OnEnable()
    {
        if (isPause)
        {
            return;
        }
        filterTarget = GetComponent<FilterTargetController>();
        bodyTypes = BodyTypes.Plants;
        DamageMultiplier = Constants.PLANES_BODY_DAMAGE_MULTIPLIER;
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
            
            if (Level >= 1)
            {
                CreateBulletPlants(tranShoot.right * 0.1f + tranShoot.up * 1f);
                CreateBulletPlants(-tranShoot.right * 0.1f + tranShoot.up * 1f);
            }
            RotateGun(target.transform.position - transform.position);
            CreateBulletPlants(tranShoot.right * 0.1f);
            CreateBulletPlants(-tranShoot.right * 0.1f);
            lastShootTime = Time.time + 1 / Asdp;
        }
    }
    void CreateBulletPlants(Vector3 offset)
    {
        BulletController bullet = Create.Instance.CreateBulletPlants(tranShoot.position + offset, tranShoot.rotation);
        bullet.Damage = Damage * DamageMultiplier;
        bullet.Level = Level;
        bullet.parent = gameObject;
        bullet.SetUp();
    }
    public override void Upgrade(int nextLevel)
    {
        switch (nextLevel)
        {
            case 1:
                Level = 1;
                DamageMultiplier = Constants.PLANES_BODY_DAMAGE_MULTIPLIER_LEVEL1;
                break;
            case 2:
                Level = 2;
                DamageMultiplier = Constants.PLANES_BODY_DAMAGE_MULTIPLIER_LEVEL2;
                break;
            case 3:
                Level = 3;
                DamageMultiplier = Constants.PLANES_BODY_DAMAGE_MULTIPLIER_LEVEL3;
                break;
        }
    }
    
}

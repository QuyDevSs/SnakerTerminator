using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDarkController : PartController
{
    public override void OnEnable()
    {
        base.OnEnable();
        bodyTypes = BodyTypes.Dark;
        DamageMultiplier = Constants.DARK_BODY_DAMAGE_MULTIPLIER;
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
            //base.Shoot();
            CreateBulletDark(Vector3.zero, 0f);
            if (Level == 1)
            {
                //float radius = Vector3.Distance(Vector3.zero, tranShoot.localPosition);
                CreateBulletDark(Vector3.zero, 180f);
            }
            else if(Level >= 2)
            {
                CreateBulletDark(Vector3.zero, 120);
                CreateBulletDark(Vector3.zero, 240);
            }
            lastShootTime = Time.time + Asdp;
        }
        
    }
    void CreateBulletDark(Vector3 offset, float angle)
    {
        BulletDark bullet = (BulletDark)Create.Instance.CreateBulletDark(tranShoot.position + offset, tranShoot.rotation);
        bullet.transform.Rotate(transform.forward, angle);
        bullet.Damage = (int)(Damage * DamageMultiplier);
        bullet.speed = Asdp;
        //bullet.radius = Vector3.Distance(Vector3.zero, tranShoot.localPosition);
        //bullet.target = gameObject;
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
                DamageMultiplier = Constants.DARK_BODY_DAMAGE_MULTIPLIER_LEVEL1;
                break;
            case 2:
                Level = 2;
                DamageMultiplier = Constants.DARK_BODY_DAMAGE_MULTIPLIER_LEVEL2;
                break;
            case 3:
                Level = 3;
                DamageMultiplier = Constants.DARK_BODY_DAMAGE_MULTIPLIER_LEVEL3;
                break;
        }
    }
}

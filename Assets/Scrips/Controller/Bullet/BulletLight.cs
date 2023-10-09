using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class BulletLight : BulletController
{
    public PartLightController partLight;
    public float lastHitTime;
    float range = 10;
    LineRenderer line;
    float damageFactor;
    // chuyển speed thành tốc độ gây sát thương
    protected override void OnEnable()
    {
        bulletTypes = BulletTypes.Light;
        line =  GetComponentInChildren<LineRenderer>();
        Level = 0;
        damageFactor = 0;
    }
    private void Update()
    {
        if (isPause)
        {
            return;
        }
        line.SetPosition(0, partLight.tranShoot.position);
        line.SetPosition(1, target.transform.position);
        if (Vector3.Distance(partLight.transform.position, target.transform.position) > range || target == null || !target.activeSelf)
        {
            EndBullet();
            partLight.isShoot = true;
        }
        else
        {
            if (lastHitTime <= Time.time)
            {
                IHit iHit = target.GetComponentInParent<IHit>();
                Damage *= (1 + damageFactor);
                iHit.OnHit(Damage, this);
                lastHitTime = Time.time + 1 / speed;
            }
        }
    }
    public override void SetUp()
    {
        if (Level == 1)
        {
            damageFactor = 0.1f;
        }
        else if (Level == 2)
        {
            damageFactor = 0.2f;
        }
        else if (Level == 3)
        {
            damageFactor = 0.3f;
        }
    }
}

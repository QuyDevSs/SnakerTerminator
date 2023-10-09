using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BulletFire : BulletController
{
    public List<GameObject> oldTargets;
    int numberBounces;
    FilterTargetController filterTarget;
    protected override void OnEnable()
    {
        oldTargets = GetComponent<CheckTargetOther>().oldTargets;
        oldTargets.Clear();

        bulletTypes = BulletTypes.Fire;
        filterTarget = GetComponent<FilterTargetController>();
        Level = 0;
        numberBounces = 2;
        //base.OnEnable();

        //Assembly assembly = Assembly.Load("Assembly-CSharp");
        //SubEffectInfo effectInfo = new SubEffectInfo();

        //effectInfo.type = assembly.GetType("EffectHPController");
        //EffectHPInfo effectHPInfo = new EffectHPInfo();
        //effectHPInfo.hp = 20;
        //effectInfo.data = effectHPInfo;

        //effectInfo.type = assembly.GetType("DotController");
        //DotInfo dotInfo = new DotInfo();
        //dotInfo.hp = 20;
        //dotInfo.time = 4f;
        //dotInfo.unitTime = 1f;
        //effectInfo.data = dotInfo;
        //List<SubEffectInfo> listsubEffectInfo = new List<SubEffectInfo>();
        //listsubEffectInfo.Add(effectInfo);
        //effectInfos = listsubEffectInfo.ToArray();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (collision.gameObject.layer == enemyLayer)
        {
            IHit iHit = collision.transform.GetComponentInParent<IHit>();
            oldTargets.Add(collision.transform.parent.gameObject);
            if (iHit != null)
            {
                iHit.OnHit(Damage, this);
                numberBounces--;
                if (numberBounces <= 0)
                {
                    EndBullet();
                    return;
                }
                if (Level >= 2)
                {
                    transform.Rotate(transform.forward, 90);
                    BulletFire newBullet = CreateBulletFire();
                    FilterTargetController newFilterTarget = newBullet.GetComponent<FilterTargetController>();
                    newBullet.target = TargetController.GetTarget(newFilterTarget);
                }
                
                target = TargetController.GetTarget(filterTarget);
            }
        }
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        if (target != null && target.gameObject.activeSelf)
        {
            transform.up = target.transform.position - transform.position;
            MoveTo(target.transform);
        }
        else
        {
            EndBullet();
        }
    }
    public override void SetUp()
    {
        if (Level >= 1)
        {
            numberBounces += 2;
        }
        if (Level >= 3)
        {
            numberBounces += 2;
        }
    }
    BulletFire CreateBulletFire()
    {
        BulletFire bullet = (BulletFire)Create.Instance.CreateBulletFire(transform.position, transform.rotation);
        bullet.transform.Rotate(Vector3.forward, 180);
        bullet.Damage = Damage;
        bullet.Level = Level - 1;
        bullet.SetUp();

        return bullet;
    }
}

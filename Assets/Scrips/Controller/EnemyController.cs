using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;

public class EnemyController : EntityController
{
    public List<SubEffectInfo> effectInfos;
    public float updateDirInterval;
    private float lastUpdateDirTime;
    public float approachDistance;
    Transform player;
    public int expRewards = 10;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        levelController.Level = 1;
        levelController.CurrentValue = 0;

        player = CreatePlayer.Instance.transform;
        lastUpdateDirTime = Time.deltaTime;
        lastShootTime = Time.deltaTime;
    }
    private void OnEnable()
    {
        hpController.CurrentValue = hpController.MaxValue;

        lastUpdateDirTime = Time.deltaTime;
        lastShootTime = Time.deltaTime;
    }
    protected override void OnDie()
    {
        float random = Random.Range(0f, 1f);
        if (random <= 0.1f)
        {
            Create.Instance.CreateItemSkills(transform.position);
        }
        Create.Instance.CreateExplosion(transform.position);
        Observer.Instance.Notify(TOPICNAME.ENEMY_DIE, this);
        PoolingObject.DestroyPooling<EnemyController>(this);
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        Vector3 direction = player.position - transform.position;
        RotateGun(direction);
        
        if (lastUpdateDirTime <= Time.time)
        {
            if (direction.magnitude > approachDistance)
            {
                body.up = direction;
            }
            else
            {
                body.up = Random.insideUnitCircle;
            }
            lastUpdateDirTime = Time.time + updateDirInterval;
        }
        Move(body.up);

        if (lastShootTime <= Time.time)
        {
            Shoot();
            lastShootTime = Time.time + 1/Asdp;
        }
    }
    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            body.up = direction;
            base.Move(direction);
        }
    }
    public void RotateGun(Vector3 direction)
    {
        if (gun != null)
        {
            gun.up = direction;
        }
    }
    public virtual void Shoot()
    {
        if (tranShoot != null)
        {
            BulletController bullet = Create.Instance.CreateBulletEnemy(tranShoot);
            bullet.Damage = Damage;
        }
    }
    protected override EntityInfo GetEntityInfo(int level)
    {
        return DataManager.Instance.enemyVO.GetEntityInfo(level);
    }
    public override void OnHit(float damage, BulletController bullet)
    {
        base.OnHit(damage, bullet);


        if (bullet.bulletTypes == BulletTypes.Circle && hpController.CurrentValue == 0)
        {
            PartController bodyPart = Create.Instance.CreateGunTurret(transform.position);
            bodyPart.Damage = bullet.Damage;
            bodyPart.Asdp = 1f;
        }

        //foreach (SubEffectInfo effectInfo in bullet.effectInfos)
        //{
        //    ItemEffect effect = (ItemEffect)gameObject.AddComponent(effectInfo.type);
        //    //effect.Info = effectInfo.data;

        //    DotInfo dotInfo = new DotInfo();
        //    dotInfo.hp = 20;
        //    dotInfo.time = 4f;
        //    dotInfo.unitTime = 1f;
        //    effect.Info = dotInfo;

        //    DotInfo dot = (DotInfo)effectInfo.data;
        //    Debug.Log("time:"+dot.time);
        //    Debug.Log("hp: "+dot.hp);
        //    Debug.Log("unit:"+dot.unitTime);
        //    effect.Active();
        //}
    }
}

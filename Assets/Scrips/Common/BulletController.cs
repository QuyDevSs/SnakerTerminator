using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
using System.Reflection;
using System;

public interface IHit
{
    void OnHit(int damage, BulletController bullet);
}
//public class Effect
//{
//    public Type type;
//    public string level;
//}
public class BulletController : MoveController
{
    public GameObject parent;
    public GameObject target;
    public float lifeTime;
    public BulletTypes bulletTypes;
    protected bool isPause;
    public int Level { get; set; }
    public int Damage { get; set; }
    protected virtual void OnEnable()
    {
        Observer.Instance.AddObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    protected void OnDisable()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    protected void EndBullet()
    {
        //Debug.Log("EndBullet");
        //Create.Instance.CreateExplosion(transform.position);
        PoolingObject.DestroyPooling<BulletController>(this);
    }
    public virtual void SetUp()
    {

    }
    protected void PauseHandle(object data)
    {
        isPause = CreateGameController.Instance.IsPause();
    }
}

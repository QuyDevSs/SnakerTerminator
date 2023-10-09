using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
using System.Reflection;
using System;

public interface IHit
{
    void OnHit(float damage, BulletController bullet);
}
public class Effect
{
    public Type type;
    public string level;
}
public class BulletController : MoveController
{
    public GameObject target;
    public float lifeTime = 3;
    public BulletTypes bulletTypes;
    protected bool isPause;
    public int Level { get; set; }
    public float Damage { get; set; }
    protected void Start()
    {
        Observer.Instance.AddObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    protected void OnDestroy()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    protected virtual void OnEnable()
    {
        StartCoroutine(DelayEndBullet(lifeTime));
    }
    private void Update()
    {
    }
    
    protected IEnumerator DelayEndBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndBullet();
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

using LTAUnityBase.Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class EntityInfo
{
    public int damage;
    public float hp;
    public float asdp;
}

public abstract class EntityController : MoveController, IHit
{
    protected bool isPause;
    public float maxSpeed;
    public EntityInfo[] entityInfos;
    public Transform gun;
    public Transform tranShoot;
    protected float lastShootTime;
    public Transform body;
    public HPController hpController;
    public LevelController levelController;
    //public TextMeshPro hitDamage;
    public int Damage { get; set; }
    public float Asdp { get; set; }
    protected abstract void OnDie();
    protected virtual void Awake()
    {
        levelController.onLevelUp = OnLevelUp;
        hpController.onDie = OnDie;
        Observer.Instance.AddObserver(TOPICNAME.PAUSE, PauseHandle);
        speed = maxSpeed;
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    public virtual void OnHit(int _damage, BulletController bullet)
    {
        hpController.TakeDamage(_damage);

        HitDamageController hitDamage = Create.Instance.CreateHitDamage(transform.position + Vector3.up * 0.5f);
        hitDamage.textMesh.text = ((int)-_damage).ToString();

        switch (bullet.bulletTypes)
        {
            case BulletTypes.Circle:
                WaveManager.totalDamages[0] += _damage;
                break;
            case BulletTypes.Normal:
                WaveManager.totalDamages[1] += _damage;
                break;
            case BulletTypes.Fire:
                WaveManager.totalDamages[2] += _damage;
                break;
            case BulletTypes.Plants:
                WaveManager.totalDamages[3] += _damage;
                break;
            case BulletTypes.Light:
                WaveManager.totalDamages[4] += _damage;
                break;
            case BulletTypes.Dark:
                WaveManager.totalDamages[5] += _damage;
                break;
        }


        if (_damage < 0)
        {
            hitDamage.textMesh.color = new Color32(0, 255, 0, 255);
        }
        else if (bullet.bulletTypes == BulletTypes.Light)
        {
            hitDamage.textMesh.color = new Color32(255, 255, 0, 255);
        }
        else if (bullet.bulletTypes == BulletTypes.Dark)
        {
            hitDamage.textMesh.color = new Color32(0, 0, 0, 255);
        }
        else if (bullet.bulletTypes == BulletTypes.Fire)
        {
            hitDamage.textMesh.color = new Color32(255, 0, 0, 255);
        }
        else if (bullet.bulletTypes == BulletTypes.Plants)
        {
            hitDamage.textMesh.color = new Color32(0, 255, 0, 255);
        }
        else if (bullet.bulletTypes == BulletTypes.Normal)
        {
            hitDamage.textMesh.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            hitDamage.textMesh.color = new Color32(255, 0, 0, 255);
        }
        //hitDamage.text = (-_damage).ToString();
        //hitDamage.gameObject.SetActive(true);
        //Invoke("DamageDisable", 1f);
    }
    
    protected virtual void OnLevelUp(int level)
    {
        EntityInfo tankInfo = GetEntityInfo(level);
        Damage = tankInfo.damage;
        hpController.HP = tankInfo.hp;
        Asdp = tankInfo.asdp;
    }
    protected void PauseHandle(object data)
    {
        isPause = CreateGameController.Instance.IsPause();
    }
    //protected bool IsBarrier()
    //{
    //    LayerMask barrierLayerMask = LayerMask.GetMask("Barrier");
    //    float distance = body.GetComponent<CircleCollider2D>().radius;
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, body.up, distance, barrierLayerMask);
    //    if (hit.transform == null)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
    protected abstract EntityInfo GetEntityInfo(int level);
}

using LTAUnityBase.Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour
{
    protected bool isPause;
    public Transform gun;
    public Transform tranShoot;
    public Transform body;
    [HideInInspector]
    public BodyTypes bodyTypes = BodyTypes.Normal;
    protected float lastShootTime;
    public int Level { get; set; }
    public float DamageMultiplier { get; set; }
    public float Damage { get; set; }
    public float Asdp { get; set; }
    protected void Start()
    {
        Observer.Instance.AddObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    public virtual void OnEnable()
    {
    }
    protected void OnDestroy()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.PAUSE, PauseHandle);
    }
    
    public virtual void Update()
    {
    }
    //public void RotateGun()
    //{
    //    if (gun == null)
    //    {
    //        return;
    //    }
    //    if (target != null)
    //    {
    //        gun.up = target.transform.position - transform.position;
    //    }
    //}
    public void RotateGun(Vector3 direction)
    {
        if (gun == null)
        {
            return;
        }
        gun.up = direction;
    }
    public virtual void Shoot()
    {
        if (tranShoot == null)
        {
            return;
        }
        Animator animator = transform.Find("Gun").GetComponent<Animator>();
        animator.Play("Shoot");
        
    }
    public virtual void Upgrade(int nextLevel)
    {

    }
    protected void PauseHandle(object data)
    {
        isPause = CreateGameController.Instance.IsPause();
    }
    //public ButtonInfo info;
    //public object Info { set => info = (ButtonInfo)value; }
    //public virtual void Upgrade()
    //{

    //}
}

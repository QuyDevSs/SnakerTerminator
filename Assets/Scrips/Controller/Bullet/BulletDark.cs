using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDark : BulletController
{
    //chuyển speed thành tốc độ đánh(tốc độ quay)
    protected override void OnEnable()
    {
        base.OnEnable();
        //lifeTime = (2 * Mathf.PI) / speed;
        bulletTypes = BulletTypes.Dark;
        lifeTime = 360f;
        Level = 0;
        //angle = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (collision.gameObject.layer == enemyLayer)
        {
            IHit iHit = collision.transform.GetComponentInParent<IHit>();
            if (iHit != null)
            {
                iHit.OnHit(Damage, this);
            }
        }
    }
    private void Update()
    {
        if (isPause)
        {
            return;
        }

        transform.position = parent.transform.position;
        float angle = speed * 360f * Time.deltaTime;
        transform.Rotate(Vector3.forward, angle);
        lifeTime -= angle;
        if (lifeTime <= 0f)
        {
            EndBullet();
        }
    }
    public override void SetUp()
    {
        if (Level == 3)
        {
            transform.localScale = new Vector3(1f, 1.5f, 1f);
        }
    }
    //public void Spin()
    //{
    //    angle += speed * 2 * Mathf.PI * Time.deltaTime;
    //    float x = target.transform.position.x + radius * Mathf.Cos(angle);
    //    float y = target.transform.position.y + radius * Mathf.Sin(angle);

    //    transform.position = new Vector3(x, y, transform.position.z);
    //    transform.up = transform.position - target.transform.position;
    //    if (angle >= 2 * Mathf.PI)
    //    {
    //        EndBullet();
    //    }
    //}
}

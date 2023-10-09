using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class CreateController : MonoBehaviour
{
    public BulletController prefabBulletEnemy, prefabBulletNormal, prefabBulletPlants, prefabBulletCircle, prefabBullet, prefabBulletFire, prefabBulletLight, prefabBulletDark;
    public ExplosionController prefabExplosion;
    public EnemyController prefabEnemy;
    public HitDamageController prefabHitDamage;
    public ItemSkillsController prefabItemSkills;
    public PartController prefabBodyPartNormal, prefabBodyPartFire, prefabBodyPartIce, prefabBodyPartPlanes, prefabBodyPartLight, prefabBodyPartDark, prefabTailPart, prefabGunTurret;

    public ExplosionController CreateExplosion(Vector2 pos)
    {
        ExplosionController explosion = PoolingObject.createPooling<ExplosionController>(prefabExplosion);

        if (explosion != null)
        {
            explosion.transform.position = pos;
            return explosion;
        }

        return Instantiate(prefabExplosion, pos, Quaternion.identity);
    }
    public HitDamageController CreateHitDamage(Vector2 pos)
    {
        HitDamageController hitDamage = PoolingObject.createPooling<HitDamageController>(prefabHitDamage);

        if (hitDamage != null)
        {
            hitDamage.transform.position = pos;
            return hitDamage;
        }

        return Instantiate(prefabHitDamage, pos, Quaternion.identity);
    }
    public ItemSkillsController CreateItemSkills(Vector2 pos)
    {
        ItemSkillsController itemSkills = PoolingObject.createPooling<ItemSkillsController>(prefabItemSkills);

        if (itemSkills != null)
        {
            itemSkills.transform.position = pos;
            return itemSkills;
        }

        return Instantiate(prefabItemSkills, pos, Quaternion.identity);
    }

    #region CreateBullet
    public BulletController CreateBulletEnemy(Transform tranShoot)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletEnemy);

        if (bullet != null)
        {
            bullet.transform.position = tranShoot.position;
            bullet.transform.rotation = tranShoot.rotation;
            return bullet;
        }
        
        return Instantiate(prefabBulletEnemy, tranShoot.position, tranShoot.rotation);
    }
    public BulletController CreateBulletNormal(Transform tranShoot)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletNormal);

        if (bullet != null)
        {
            bullet.transform.position = tranShoot.position;
            bullet.transform.rotation = tranShoot.rotation;
            return bullet;
        }

        return Instantiate(prefabBulletNormal, tranShoot.position, tranShoot.rotation);
    }
    public BulletController CreateBullet(Transform tranShoot)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBullet);

        if (bullet != null)
        {
            bullet.transform.position = tranShoot.position;
            bullet.transform.rotation = tranShoot.rotation;
            return bullet;
        }

        return Instantiate(prefabBullet, tranShoot.position, tranShoot.rotation);
    }
    public BulletController CreateBulletCircle(Vector2 pos)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletCircle);

        if (bullet != null)
        {
            bullet.transform.position = pos;
            return bullet;
        }

        BulletController newBullet = Instantiate(prefabBulletCircle, pos, Quaternion.identity);
        return newBullet;
    }
    public BulletController CreateBulletPlants(Vector3 pos, Quaternion quaternion)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletPlants);

        if (bullet != null)
        {
            bullet.transform.position = pos;
            bullet.transform.rotation = quaternion;
            return bullet;
        }

        return Instantiate(prefabBulletPlants, pos, quaternion);
    }
    public BulletController CreateBulletFire(Vector3 pos, Quaternion quaternion)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletFire);

        if (bullet != null)
        {
            bullet.transform.position = pos;
            bullet.transform.rotation = quaternion;
            return bullet;
        }

        return Instantiate(prefabBulletFire, pos, quaternion);
    }
    public BulletController CreateBulletDark(Vector3 pos, Quaternion quaternion)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletDark);
        if (bullet != null)
        {
            bullet.transform.position = pos;
            bullet.transform.rotation = quaternion;
            return bullet;
        }

        return Instantiate(prefabBulletDark, pos, quaternion);
    }
    public BulletController CreateBulletLight()//Transform tranShoot)
    {
        BulletController bullet = PoolingObject.createPooling<BulletController>(prefabBulletLight);

        if (bullet != null)
        {
            bullet.transform.position = Vector3.zero;
            bullet.transform.rotation = Quaternion.identity;//tranShoot.rotation;
            return bullet;
        }

        return Instantiate(prefabBulletLight, Vector3.zero, Quaternion.identity);
    }
    
    public EnemyController CreateEnemy(Vector3 pos)
    {
        EnemyController enemy = PoolingObject.createPooling<EnemyController>(prefabEnemy);

        if (enemy != null)
        {
            enemy.transform.position = pos;
            enemy.transform.rotation = Quaternion.identity;
            return enemy;
        }
        return Instantiate(prefabEnemy, pos, Quaternion.identity);
    }
    #endregion
    
    #region CreateBodyPart
    public PartController CreateBodyPartNormal(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartNormal);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartNormal, pos, rota);
    }
    public PartController CreateBodyPartFire(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartFire);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartFire, pos, rota);
    }
    public PartController CreateBodyPartIce(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartIce);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartIce, pos, rota);
    }
    public PartController CreateBodyPartPlants(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartPlanes);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartPlanes, pos, rota);
    }
    public PartController CreateBodyPartLight(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartLight);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartLight, pos, rota);
    }
    public PartController CreateBodyPartDark(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabBodyPartDark);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabBodyPartDark, pos, rota);
    }
    public PartController CreateTailPart(Vector3 pos, Quaternion rota)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabTailPart);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = rota;
            return bodyPart;
        }

        return Instantiate(prefabTailPart, pos, rota);
    }
    public PartController CreateGunTurret(Vector3 pos)
    {
        PartController bodyPart = PoolingObject.createPooling<PartController>(prefabGunTurret);

        if (bodyPart != null)
        {
            bodyPart.transform.position = pos;
            bodyPart.transform.rotation = Quaternion.identity;
            return bodyPart;
        }

        return Instantiate(prefabGunTurret, pos, Quaternion.identity);
    }
    #endregion
}

public class Create : SingletonMonoBehaviour<CreateController>
{
}



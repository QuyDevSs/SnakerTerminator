using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
using TMPro;
public class HitDamageController : MonoBehaviour
{
    float lifeTime;
    public TextMeshPro textMesh;
    private void OnEnable()
    {
        lifeTime = 1f;
        transform.position = Vector3.zero;
    }

    void Update()
    {
        if (lifeTime <= 0)
        {
            EndHitDamage();
        }
        transform.position += Vector3.up * 0.01f;
        lifeTime -= Time.deltaTime;
        
    }
    void EndHitDamage()
    {
        PoolingObject.DestroyPooling<HitDamageController>(this);
    }
}

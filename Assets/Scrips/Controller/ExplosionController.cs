using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class ExplosionController : MonoBehaviour
{
    private void OnEnable()
    {
        //this.GetComponent<Animator>().Play("Explosion");
    }
    public void EndExplosion()
    {
        PoolingObject.DestroyPooling<ExplosionController>(this);
    }
}

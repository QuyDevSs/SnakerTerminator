using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ICondition
{
    public bool IsSuitable { get; }
    public Action<ICondition> onSuitable { set; }
    public void ResetCondition();
}
public interface IHandle
{
    public string Handle { get; }
}
public class SkillController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

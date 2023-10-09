using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class ButtonManager : MonoBehaviour
{
    public JoyStickController joyStick;
    public SelectSkillsController selectSkills;
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class CreateButtonManager : SingletonMonoBehaviour<ButtonManager>
{
}

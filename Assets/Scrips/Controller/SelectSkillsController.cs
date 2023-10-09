using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Events;
using System;

public class ButtonInfo
{
    public string part;
    public string text;
    public string function;
}
//public interface PartUpgrade
//{
//    public object Info { set; }
//    public void Upgrade();
//}
public class SelectSkillsController : MonoBehaviour
{
    SubEffectInfo[] btnInfos;
    public Button[] btnSkills;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/BtnInfos").text);
        JSONArray array = json["data"].AsArray;
        btnInfos = Utils.GetSubEffectInfos(array);

        CreateGameController.Instance.Pause();
        for (int i = 0; i < btnSkills.Length; i++)
        {
            Image buttonImage = btnSkills[i].GetComponent<Image>();
            TextMeshProUGUI btnText = btnSkills[i].GetComponentInChildren<TextMeshProUGUI>();

            int randomIndex = UnityEngine.Random.Range(0, btnInfos.Length);
            ButtonInfo buttonInfo = (ButtonInfo)btnInfos[randomIndex].data;

            buttonImage.sprite = Resources.Load<Sprite>(buttonInfo.part);
            btnText.text = buttonInfo.text;

            UnityAction action = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), CreatePlayer.Instance, buttonInfo.function);
            btnSkills[i].onClick.AddListener(action);
            btnSkills[i].onClick.AddListener(Disable);
        }
    }
    void Disable()
    {
        gameObject.SetActive(false);
        CreateGameController.Instance.Pause();
    }
    private void OnDisable()
    {
        for (int i = 0; i < btnSkills.Length; i++)
        {
            btnSkills[i].onClick.RemoveAllListeners();
        }
    }
}

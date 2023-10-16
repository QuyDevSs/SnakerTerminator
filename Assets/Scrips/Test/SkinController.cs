using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinInfo
{
    public int index;
    public string off;
    public string on;
}
public class SkinController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    int distance = 840;
    public GameObject prefabSkin;
    public RectTransform skins;
    public Button pick;
    SkinInfo[] skinInfos;
    int[] skinsUnlock;
    int skinCurrent;
    int skinDisplay;
    bool isDrag;

    const string KEY_SKIN_CURRENT = "SkinCurrent";
    const string KEY_SKIN_UNLOCK = "SkinUnlock";

    ScrollRect scrollRect;
    Vector2 dragStartPosition;
    public int SkinDisplay
    {
        set
        {
            if (value < 0)
            {
                skinDisplay = 0;
            }
            else if (value > skinInfos.Length - 1)
            {
                skinDisplay = skinInfos.Length - 1;
            }
            else
            {
                skinDisplay = value;
            }
        }
        get
        {
            return skinDisplay;
        }
    }

    public int SkinCurrent
    {
        set
        {
            if (value < 0)
            {
                skinCurrent = 0;
            }
            if (value >= skinsUnlock.Length)
            {
                return;
            }
            else if (skinsUnlock[value] == 1)
            {
                skinCurrent = value;
            }
        }
        get
        {
            return skinCurrent;
        }
    }
    private void OnEnable()
    {
        JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/skinInfo").text);
        JSONArray array = json["data"].AsArray;
        List<SkinInfo> listSkinInfo = new List<SkinInfo>();
        for (int i = 0; i < array.Count; i++)
        {
            JSONNode jsonNode = array[i];
            SkinInfo skinInfo = JsonUtility.FromJson<SkinInfo>(jsonNode.ToString());
            listSkinInfo.Add(skinInfo);
        }
        skinInfos = listSkinInfo.ToArray();

        string skinsUnlockStr = PlayerPrefs.GetString(KEY_SKIN_UNLOCK, "100000000000000000000");
        skinsUnlock = new int[skinInfos.Length];
        for (int i = 0; i < skinsUnlock.Length; i++)
        {
            skinsUnlock[i] = int.Parse(skinsUnlockStr[i].ToString());
        }

        for (int i = 0; i < skinInfos.Length; i++)
        {
            GameObject player = Instantiate(prefabSkin, skins);
            RectTransform rectTransform = player.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(540 + distance * i, rectTransform.anchoredPosition.y);
            if (skinsUnlock[i] != 0)
            {
                player.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(skinInfos[i].on);
                player.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                player.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(skinInfos[i].off);
            }
        }

        skins.sizeDelta = new Vector2(1080 + (skinInfos.Length - 1) * distance, skins.sizeDelta.y);

        scrollRect = GetComponent<ScrollRect>();
        isDrag = false;

        SkinCurrent = PlayerPrefs.GetInt(KEY_SKIN_CURRENT, 0);
        SkinDisplay = SkinCurrent;
    }
    public void NextSkin()
    {
        SkinDisplay++;
        //skins.position += new Vector3(-5, 0, 0);
    }
    public void PrevSkin()
    {
        SkinDisplay--;
        //skins.position += new Vector3(5, 0, 0);
    }
    public void UnlockAndPick()
    {
        if (skinsUnlock[SkinDisplay] == 1)
        {
            SkinCurrent = SkinDisplay;
            PlayerPrefs.SetInt(KEY_SKIN_CURRENT, SkinCurrent);
            PlayerPrefs.Save();
            pick.gameObject.SetActive(false);
        }
        else
        {
            skinsUnlock[SkinDisplay] = 1;
            string skinsUnlockStr = PlayerPrefs.GetString(KEY_SKIN_UNLOCK, "100000000000000000000");
            char[] chars = skinsUnlockStr.ToCharArray();
            chars[SkinDisplay] = '1';
            skinsUnlockStr = new string(chars);
            PlayerPrefs.SetString(KEY_SKIN_UNLOCK, skinsUnlockStr);
            PlayerPrefs.Save();

            skins.GetChild(SkinDisplay).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(skinInfos[SkinDisplay].on);
            skins.GetChild(SkinDisplay).GetChild(1).gameObject.SetActive(false);
            pick.GetComponentInChildren<Text>().text = "Pick";
        }
        
    }
    private void Update()
    {
        if (SkinDisplay == SkinCurrent)
        {
            pick.gameObject.SetActive(false);
        }
        else if(skinsUnlock[SkinDisplay] == 1)
        {
            pick.gameObject.SetActive(true);
            pick.GetComponentInChildren<Text>().text = "Pick";
        }
        else
        {
            pick.gameObject.SetActive(true);
            pick.GetComponentInChildren<Text>().text = "Unlock";
        }

        if (isDrag || skinInfos.Length <= 1)
        {
            return;
        }
        float ratio = (float)SkinDisplay / (skinInfos.Length - 1);
        if (scrollRect.normalizedPosition.x != ratio)
        {
            float distance = Mathf.Abs(scrollRect.normalizedPosition.x - ratio);
            float speed = distance / 0.2f;
            float newPositionX = Mathf.MoveTowards(scrollRect.normalizedPosition.x, ratio, speed * Time.deltaTime);
            scrollRect.normalizedPosition = new Vector2(newPositionX, scrollRect.normalizedPosition.y);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragEndPosition = eventData.position;
        float delta = dragEndPosition.x - dragStartPosition.x;
        if (delta > 100f)
        {
            SkinDisplay = Mathf.FloorToInt(scrollRect.normalizedPosition.x * (skinInfos.Length - 1)); // Làm tròn xuống
        }
        else if (delta < -100f)
        {
            SkinDisplay = Mathf.CeilToInt(scrollRect.normalizedPosition.x * (skinInfos.Length - 1)); // Làm tròn lên
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        dragStartPosition = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }
}

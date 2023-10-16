using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelInfo
{
    public int lv;
    public string part;
}
public class LevelManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    LevelInfo[] levelInfos;
    
    int levelDisplay;
    RectTransform content;
    public float distance = 680;
    public GameObject prefabLevel;
    GameObject play;
    bool isDrag;
    ScrollRect scrollRect;
    Vector2 dragStartPosition;

    public int LevelDisplay
    {
        set
        {
            int levelUnlock = CreateGameController.Instance.LevelUnlock;
            if (value < 0)
            {
                levelDisplay = 0;
            }
            else if (value > levelUnlock + 1)
            {
                levelDisplay = levelUnlock + 1;
            }
            else
            {
                levelDisplay = value;
            }
            OnChangeLevelDisplay();
        }
        get
        {
            return levelDisplay;
        }
    }

    private void Awake()
    {
        JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/LevelInfo").text);
        JSONArray array = json["data"].AsArray;
        List<LevelInfo> listLevelInfo = new List<LevelInfo>();
        for (int i = 0; i < array.Count; i++)
        {
            JSONNode jsonNode = array[i];
            LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(jsonNode.ToString());
            listLevelInfo.Add(levelInfo);
        }
        levelInfos = listLevelInfo.ToArray();

        content = transform.GetChild(0).GetComponent<RectTransform>();

        for (int i = 0; i < levelInfos.Length; i++)
        {
            GameObject levelObject = Instantiate(prefabLevel, content);
            RectTransform rectTransform = levelObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(540 + distance * i, rectTransform.anchoredPosition.y);
            levelObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(levelInfos[i].part);
            levelObject.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + levelInfos[i].lv.ToString();
            if (i <= CreateGameController.Instance.LevelUnlock)
            {
                levelObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        scrollRect = GetComponent<ScrollRect>();
    }
    private void OnEnable()
    {
        CreateGameController.Instance.gameState = GameStates.Menu;
        play = transform.GetChild(1).gameObject;
        CreateGameController.Instance.LevelCurrent = CreateGameController.Instance.LevelUnlock;
        LevelDisplay = CreateGameController.Instance.LevelCurrent;
        content.sizeDelta = new Vector2(1080 + (CreateGameController.Instance.LevelUnlock + 1) * distance, content.sizeDelta.y);
        content.GetChild(CreateGameController.Instance.LevelUnlock).GetChild(1).gameObject.SetActive(false);
        
        isDrag = false;
    }
    void OnChangeLevelDisplay()
    {
        int levelCurrent = CreateGameController.Instance.LevelCurrent;
        //int levelUnlock = CreateGameController.Instance.LevelUnlock;
        if (LevelDisplay == levelCurrent)
        {
            play.SetActive(true);
        }
        else
        {
            play.SetActive(false);
        }
    }
    void Update()
    {
        if (isDrag)
        {
            return;
        }
        //Debug.Log(LevelDisplay);
        //Debug.Log("Current:" + LevelCurrent);
        //Debug.Log("Unlock: " + CreateGameController.Instance.LevelUnlock);
        float ratio = (float)LevelDisplay / (CreateGameController.Instance.LevelUnlock + 1);
        if (scrollRect.normalizedPosition.x != ratio)
        {
            float distance = Mathf.Abs(ratio - scrollRect.normalizedPosition.x);
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
            LevelDisplay = Mathf.FloorToInt(scrollRect.normalizedPosition.x * (CreateGameController.Instance.LevelUnlock + 1)); // Làm tròn xuống
        }
        else if (delta < -100f)
        {
            LevelDisplay = Mathf.CeilToInt(scrollRect.normalizedPosition.x * (CreateGameController.Instance.LevelUnlock + 1)); // Làm tròn lên
        }
        CreateGameController.Instance.LevelCurrent = LevelDisplay;
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
    public void LoadLevelAsync()
    {
        CreateGameController.Instance.LoadLevelAsync();
    }
}

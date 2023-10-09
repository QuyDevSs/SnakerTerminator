using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo
{
    public int lv;
    public string part;
}
public class LevelManager : MonoBehaviour
{
    LevelInfo[] levelInfos;
    public Image image;
    public TextMeshProUGUI textMesh;
    public Button btnNextLv;
    public Button btnPrevLv;
    int levelCurrent;
    public int LevelCurrent
    {
        set
        {
            if (value < 0)
            {
                levelCurrent = 0;
            }
            else if (value > 1)
            {
                levelCurrent = 1;
            }
            else
            {
                levelCurrent = value;
            }
        }
        get
        {
            return levelCurrent;
        }
    }
    private void Start()
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

        if (PlayerPrefs.HasKey(Constants.LEVEL_CURRENT))
        {
            LevelCurrent = PlayerPrefs.GetInt(Constants.LEVEL_CURRENT);
        }
        else
        {
            LevelCurrent = 0;
        }

        DisPlayLevelCurrent();
    }
    public void DisPlayLevelCurrent()
    {
        image.sprite = Resources.Load<Sprite>(levelInfos[LevelCurrent].part);
        textMesh.text = "Level " + levelInfos[LevelCurrent].lv.ToString();
        
        if (LevelCurrent == 1)
        {
            btnPrevLv.gameObject.SetActive(true);
        }
        if (LevelCurrent == 0)
        {
            btnPrevLv.gameObject.SetActive(false);
        }
        if (LevelCurrent == levelInfos.Length - 2)
        {
            btnNextLv.gameObject.SetActive(true);
        }
        if (LevelCurrent == levelInfos.Length - 1)
        {
            btnNextLv.gameObject.SetActive(false);
        }
    }
    public void NextLevel()
    {
        LevelCurrent++;
        DisPlayLevelCurrent();
    }
    public void PrevLevel()
    {
        LevelCurrent--;
        DisPlayLevelCurrent();
    }
    public void LoadLevelAsync()
    {
        CreateGameController.Instance.LoadLevelAsync(LevelCurrent + 1);
    }
}

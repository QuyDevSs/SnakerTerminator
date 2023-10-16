using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsInfo
{
    public TextMeshProUGUI textMesh;
    public Transform bg;
    public Transform handle;


}
public class StatsController : MonoBehaviour
{
    public StatsInfo[] statsInfos;
    public GameObject[] childObjects;
    
    private void OnEnable()
    {
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }

        statsInfos = new StatsInfo[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            statsInfos[i] = new StatsInfo();
            statsInfos[i].textMesh = childObjects[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            statsInfos[i].bg = childObjects[i].transform.GetChild(1);
            statsInfos[i].handle = childObjects[i].transform.GetChild(2);
        }

    }
    public void UpdateStats(int[] numbers)
    {
        int maxInt = Utils.FindMaxNumber(numbers);

        for (int i = 0; i < transform.childCount; i++)
        {
            statsInfos[i].textMesh.text = numbers[i].ToString();
            statsInfos[i].handle.localScale = new Vector3((float)(numbers[i]) / maxInt, 1, 2);
        }
    }
}

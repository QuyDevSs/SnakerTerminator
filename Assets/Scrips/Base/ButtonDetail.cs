using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class ButtonDetail : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI textMesh;
    public string text;
    public void OnPointerDown(PointerEventData eventData)
    {
        textMesh.gameObject.SetActive(true);
        textMesh.text = text;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        textMesh.gameObject.SetActive(false);
    }
}

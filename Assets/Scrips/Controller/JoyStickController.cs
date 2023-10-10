using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class JoyStickController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 direction;

    Vector3 posJoyStick;

    [SerializeField]
    Transform JoyStick;

    [SerializeField]
    Transform BgJoyStick;

    Vector3 OriginalPos;
    Action<Vector3> endDrag;

    public Action<Vector3> EndDrag
    {
        set
        {
            endDrag = value;
        }
    }

    float maxLength = 70f;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public Vector3 PosJoyStick
    {
        get
        {
            return posJoyStick;
        }
    }

    public float MaxLegnth
    {
        get
        {
            return maxLength;
        }
    }

    protected virtual void Start()
    {
        OriginalPos = BgJoyStick.transform.position;
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector3 canvasScale = canvas.transform.localScale;
        float size = BgJoyStick.GetComponent<RectTransform>().rect.width / 2;
        maxLength = size * canvasScale.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BgJoyStick.position = eventData.position;
        direction = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveJoyStick(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (endDrag != null)
            endDrag(direction);
        direction = Vector3.zero;
        JoyStick.transform.localPosition = Vector3.zero;
        BgJoyStick.position = OriginalPos;
        posJoyStick = JoyStick.localPosition;
    }

    void MoveJoyStick(Vector3 touchPos)
    {
        Vector2 offset = touchPos - BgJoyStick.position;
        Vector3 realdirection = Vector2.ClampMagnitude(offset, maxLength);
        direction = realdirection.normalized;
        JoyStick.position = new Vector3(BgJoyStick.position.x + realdirection.x, BgJoyStick.position.y + realdirection.y, 0);

        posJoyStick = JoyStick.localPosition;
    }

}

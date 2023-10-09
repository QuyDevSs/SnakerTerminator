using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LearnUIUnity : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform handle;
    public Transform BgJoyStick;
    Vector3 PosHandle;
    private void Start()
    {
        PosHandle = handle.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dir;
        dir.x = eventData.position.x - this.transform.position.x;
        dir.y = eventData.position.y - this.transform.position.y;
        float range = dir.magnitude;
        if (range <= 50)
        {
            handle.transform.position = eventData.position;
        }
        else
        {
            handle.transform.localPosition = (200 / range) * dir;
        }
        Vector3 dirPlayer = handle.transform.position - PosHandle;
        CreatePlayer.Instance.body.up = dirPlayer;
        //if (range <= 200)
        //{
        //    handle.transform.position = eventData.position;
        //}
        //else
        //{
        //    //float rangeMouse = eventData.position.magnitude;
        //    handle.transform.position = (200/ range) * eventData.position;
        //}
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //handle.transform.position = eventData.position;
        //float range = handle.transform.localPosition.magnitude;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        handle.transform.position = PosHandle;
        //handle.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");
        //handle.transform.position = eventData.position;
    }

    //void moveJoyStick(Vector3 touchPos)
    //{
    //    Vector2 offset = touchPos - BgJoyStick.position;
    //    Vector3 realdirection = Vector2()
    //}
}

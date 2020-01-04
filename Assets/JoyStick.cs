using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public RectTransform bgTransform;
    public RectTransform centerTransform;
    public float R;
    public Camera uiCamera;

    public Action UIPointUp;
    public Action<Vector2> UIDrag;
    public Action UIPointDown;

    Vector2 bgOffsetPos;

    private void Awake()
    {

        R = bgTransform.rect.width / 2;
        bgOffsetPos = new Vector2(R, R);
    }


    Vector2 GetUIPos(Vector2 viewPos)
    {

         RectTransformUtility.ScreenPointToLocalPointInRectangle(bgTransform, viewPos, uiCamera, out Vector2 localPoint);
        return localPoint-bgOffsetPos;
    }

    public void OnDrag(PointerEventData eventData)
    {

        var pos = GetUIPos(eventData.position);
        if(pos.magnitude>R)
        {
            pos = pos.normalized * R;
        }
        centerTransform.anchoredPosition = pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        centerTransform.anchoredPosition =GetUIPos( eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        centerTransform.anchoredPosition = new Vector2(0, 0);
    }
}

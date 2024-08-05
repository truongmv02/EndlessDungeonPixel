using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIClick : MonoBehaviour
{

    public RectTransform content;

    public Action InsideCallBack;
    public Action OutsideCallBack;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick(Input.mousePosition);
        }
    }

    void OnClick(Vector2 position)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
       content, position))
        {
            if (InsideCallBack != null)
            {
                InsideCallBack();
            }
        }
        else
        {
            if (OutsideCallBack != null)
            {
                OutsideCallBack();
            }
        }

    }
}

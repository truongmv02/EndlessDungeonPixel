using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeController : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public int MaxPage { set; get; }
    public int CurrentPage { set; get; }
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform pagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    float dragThreshould;
    public event Action<int> OnChangeValue;

    private void Awake()
    {
        CurrentPage = 0;
        targetPos = pagesRect.localPosition;
        dragThreshould = Screen.width / 15f;
    }

    public void MovePage(float delay = 0f)
    {
        targetPos = CurrentPage * pageStep;
        pagesRect.LeanMoveLocal(targetPos, delay).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetCurrentPage();
        MovePage(tweenTime);
    }

    void SetCurrentPage()
    {
        var currentPos = pagesRect.localPosition * -1;
        var step = pageStep * -1;
        var currentPage = CurrentPage;
        float offset = currentPos.x % step.x;

        if (currentPos.x < step.x / 2f)
        {
            CurrentPage = 0;
        }
        else if (currentPos.x > (step.x * (MaxPage - 1) - step.x / 2f))
        {
            CurrentPage = MaxPage - 1;
        }
        else
        {
            CurrentPage = (int)(currentPos.x / step.x);
        }

        if (CurrentPage < MaxPage - 1 && (offset > step.x / 2f))
        {
            CurrentPage++;
        }

        if (currentPage != CurrentPage)
        {
            OnChangeValue?.Invoke(CurrentPage);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetCurrentPage();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{

    [SerializeField] GameObject minimapSmall;
    [SerializeField] GameObject minimapLarge;

    RectTransform minimapSmallRectTransform;
    RectTransform minimapLargeRectTransform;
    UIClick uiClick;
    private void Awake()
    {
        uiClick = GetComponent<UIClick>();
        minimapSmallRectTransform = minimapSmall.GetComponent<RectTransform>();
        minimapLargeRectTransform = minimapLarge.GetComponent<RectTransform>();
    }

    private void Start()
    {
        CloseMinimapLarge();
    }


    void OpenMinimapLarge()
    {
        uiClick.content = minimapLargeRectTransform;
        uiClick.InsideCallBack = null;
        uiClick.OutsideCallBack = CloseMinimapLarge;
        minimapLarge.SetActive(true);
        minimapSmall.SetActive(false);
        GameManager.Instance.MiniMap.CinemachineVirtualCamera.m_Lens.OrthographicSize = 100;
    }

    void CloseMinimapLarge()
    {
        uiClick.content = minimapSmallRectTransform;
        uiClick.InsideCallBack = OpenMinimapLarge;
        uiClick.OutsideCallBack = null;
        minimapLarge.SetActive(false);
        minimapSmall.SetActive(true);
        GameManager.Instance.MiniMap.CinemachineVirtualCamera.m_Lens.OrthographicSize = 50;
    }



}

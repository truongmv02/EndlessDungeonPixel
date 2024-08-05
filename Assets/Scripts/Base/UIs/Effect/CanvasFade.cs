using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFade : UIEffect
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Appear()
    {
        base.Appear();
        canvasGroup.alpha = 0f;
        LeanTween.alphaCanvas(canvasGroup, 1, animationTime).setIgnoreTimeScale(true);
    }

    public override void Disappear()
    {
        base.Disappear();
        LeanTween.alphaCanvas(canvasGroup, 0, animationTime).setIgnoreTimeScale(true);
    }
}

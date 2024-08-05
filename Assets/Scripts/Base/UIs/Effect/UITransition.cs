using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UITransition : UIEffect
{

    public Vector3 appearPosition;
    public Vector3 mainPosition;
    public Vector3 disappearPosition;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void Appear()
    {
        base.Appear();
        rectTransform.localPosition = appearPosition;
        rectTransform.LeanMove(appearPosition, 0).setIgnoreTimeScale(true);
        rectTransform.LeanMove(mainPosition, animationTime).setIgnoreTimeScale(true);
    }

    public override void Disappear()
    {
        base.Disappear();
        rectTransform.LeanMove(disappearPosition, animationTime).setIgnoreTimeScale(true);

    }
}


using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererFade : UIEffect
{
    public override void Appear()
    {
        base.Appear();
        LeanTween.alpha(gameObject, 1, animationTime).setIgnoreTimeScale(true);
    }

    public override void Disappear()
    {
        base.Disappear();
        LeanTween.alpha(gameObject, 0, animationTime).setIgnoreTimeScale(true);
    }
}

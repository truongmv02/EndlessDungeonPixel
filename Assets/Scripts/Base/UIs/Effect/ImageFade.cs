using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : UIEffect
{
    [SerializeField] float minValue = 0f;
    [SerializeField] float maxValue = 0.95f;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public override void Appear()
    {
        base.Appear();
        LeanTween.value(minValue, maxValue, animationTime).setOnUpdate((float value) =>
        {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }).setEase(LeanTweenType.easeInQuad).setIgnoreTimeScale(true);
    }

    public override void Disappear()
    {
        base.Disappear();
        LeanTween.value(maxValue, minValue, animationTime).setOnUpdate((float value) =>
        {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }).setEase(LeanTweenType.easeInQuad).setIgnoreTimeScale(true);
    }

}

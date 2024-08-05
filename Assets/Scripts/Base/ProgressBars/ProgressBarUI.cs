using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : ProgressBarBase
{
    Image backgroundImage;
    Image handleImage;

    private void Awake()
    {
        backgroundImage = transform.Find("Background")?.GetComponent<Image>();
        handleImage = transform.Find("Handle").GetComponent<Image>();
    }

    public void SetSprite(Sprite background, Sprite handle)
    {
        backgroundImage.sprite = background;
        handleImage.sprite = handle;
    }

    protected override void UpdateProgress()
    {
        handleImage.fillAmount = currentValue / totalValue;
    }
}

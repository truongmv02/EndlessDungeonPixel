using System.Collections;
using UnityEngine;

public class ProgressBar : ProgressBarBase
{
    public SpriteRenderer BackgroundSpriteRenderer { get; protected set; }
    public SpriteRenderer HandleSpriteRenderer { get; protected set; }

    private void Awake()
    {
        BackgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        HandleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool flip = transform.rotation.y < 0 ? true : false;
        if (flip)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Hide()
    {
        BackgroundSpriteRenderer.enabled = false;
        HandleSpriteRenderer.enabled = false;
    }

    public void Display()
    {
        BackgroundSpriteRenderer.enabled = true;
        HandleSpriteRenderer.enabled = true;
    }

    protected override void UpdateProgress()
    {
        var scale = new Vector3(currentValue / totalValue, HandleSpriteRenderer.transform.localScale.y, HandleSpriteRenderer.transform.localScale.z);
        HandleSpriteRenderer.transform.localScale = scale;
    }
}

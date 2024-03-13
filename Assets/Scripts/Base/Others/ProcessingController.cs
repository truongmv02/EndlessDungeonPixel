using System.Collections;
using UnityEngine;

public class ProcessingController : MonoBehaviour
{
    public SpriteRenderer BackgroundSpriteRenderer { get; protected set; }
    public SpriteRenderer HandleSpriteRenderer { get; protected set; }

    private float currentValue;
    public float MaxValue { set; get; }
    public float CurrentValue
    {
        set
        {
            currentValue = Mathf.Clamp(value, 0, MaxValue);
            var scale = new Vector3(currentValue / MaxValue, HandleSpriteRenderer.transform.localScale.y, HandleSpriteRenderer.transform.localScale.z);
            HandleSpriteRenderer.transform.localScale = scale;
        }
        get
        {
            return currentValue;
        }
    }
    private void Awake()
    {
        BackgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        HandleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
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


}

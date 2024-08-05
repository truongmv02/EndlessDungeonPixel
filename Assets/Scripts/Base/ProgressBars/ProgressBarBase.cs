using System.Collections;
using UnityEngine;

public abstract class ProgressBarBase : MonoBehaviour
{

    protected float totalValue = 1;
    protected float currentValue = 1;

    public float TotalValue
    {
        get => totalValue;
        set
        {
            totalValue = value;
            UpdateProgress();
        }
    }
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, 0, totalValue);
            UpdateProgress();
        }
    }

    public void SetValue(float currentValue, float totalValue)
    {
        this.currentValue = currentValue;
        this.totalValue = totalValue;
        UpdateProgress();
    }

    protected abstract void UpdateProgress();
}

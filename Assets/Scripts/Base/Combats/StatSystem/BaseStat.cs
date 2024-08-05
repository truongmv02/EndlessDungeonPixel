using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatInfos
{
    public StatInfo[] stats;
}


[System.Serializable]
public class StatInfo
{
    public string statName;
    public float value;
}

[System.Serializable]
public class BaseStat
{
    [field: SerializeField] public string StatName { get; set; }
    protected float value;
    public event Action<float> OnValueChange;
    public event Action OnValueZero;
    public float Value
    {
        set
        {
            if (this.value <= 0 && value <= 0) return;
            this.value = Mathf.Clamp(value, 0, Mathf.Infinity);
            OnValueChange?.Invoke(this.value);
            if (this.value <= 0) OnValueZero?.Invoke();
        }
        get => value;
    }

}

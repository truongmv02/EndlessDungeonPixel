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
    public float Value
    {
        set
        {
            this.value = Mathf.Clamp(value, 0, Mathf.Infinity);
            OnValueChange?.Invoke(this.value);
        }
        get => value;
    }

}

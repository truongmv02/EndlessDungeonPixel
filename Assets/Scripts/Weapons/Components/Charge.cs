using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ChargeInfo
{
    public float chargeTime;
    public int chargeAmount = 1;
    public int initialChargeAmount = 0;
}

public class Charge : BaseComponent<ChargeInfo>
{
    public int CurrentCharge { get; private set; }
    public BaseStat ChargeTime { get; set; } = new BaseStat();
    public BaseStat ChargeAmount { get; set; } = new BaseStat() { Value = 1 };

    private bool canCharge;
    public float timer;

    public event Action<int> OnCurrentChargeChange;

    private void Awake()
    {
    }

    public void StartCharge()
    {
        timer = 0;
        canCharge = true;
    }

    public void StopCharge()
    {
        CurrentCharge = 0;
        canCharge = false;
    }

    private void Update()
    {
        if (!canCharge) return;
        timer += Time.deltaTime;
        if (timer >= ChargeTime.Value)
        {
            CurrentCharge = Mathf.Clamp(CurrentCharge + 1, 0, (int)ChargeAmount.Value);
            timer = 0;
            OnCurrentChargeChange?.Invoke(CurrentCharge);
        }
        if (CurrentCharge == (int)ChargeAmount.Value)
        {
            canCharge = false;
        }
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        CurrentCharge = Info.initialChargeAmount;
        ChargeAmount.Value = Info.chargeAmount;
        ChargeTime.Value = Info.chargeTime;
    }
}

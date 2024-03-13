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
    private bool canCharge;
    public float CurrentChargeTime { get; private set; }

    public event Action<int> OnCurrentChargeChange;

    public event Action OnStartCharge;
    public event Action OnStopCharge;
    public event Action<float, int> OnChargeUpdate;

    public void StartCharge()
    {
        CurrentChargeTime = 0;
        canCharge = true;
        OnStartCharge?.Invoke();
    }

    public void StopCharge()
    {
        CurrentCharge = 0;
        canCharge = false;
        OnStopCharge?.Invoke();
    }

    private void Update()
    {
        if (!canCharge) return;
        CurrentChargeTime += Time.deltaTime;
        if (CurrentChargeTime >= Info.chargeTime)
        {
            CurrentCharge = Mathf.Clamp(CurrentCharge + 1, 0, Info.chargeAmount);
            CurrentChargeTime = 0;
            OnCurrentChargeChange?.Invoke(CurrentCharge);
        }
        if (CurrentCharge == Info.chargeAmount)
        {
            canCharge = false;
        }

        OnChargeUpdate?.Invoke(CurrentChargeTime, CurrentCharge);
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
    }
}

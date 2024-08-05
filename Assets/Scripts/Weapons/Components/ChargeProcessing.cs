using System.Collections;
using UnityEngine;

[System.Serializable]
public class ChargeProcessingInfo
{
    public string backgroundSprite;
    public string handleSprite;
}
public class ChargeProcessing : BaseComponent<ChargeProcessingInfo>
{
    Sprite backgroundSprite;
    Sprite handleSprite;
    ProgressBar processing;
    Charge charge;

    private void Start()
    {
        processing = GetComponentInChildren<ProgressBar>();
        charge = GetComponent<Charge>();

        BaseUtils.ValidateCheckNullValue(processing, nameof(processing), nameof(ChargeProcessing), name);
        BaseUtils.ValidateCheckNullValue(charge, nameof(charge), nameof(ChargeProcessing), name);

        InitInfo();
        charge.OnStartCharge += HandleStartCharge;
        charge.OnChargeUpdate += HandleChargeUpdate;
        charge.OnStopCharge += HandleStopCharge;
    }

    private void HandleStartCharge()
    {
        processing.Display();
    }

    private void HandleChargeUpdate(float currentChargeTime, int currentAmount)
    {
        float totalValue = charge.Info.chargeAmount * charge.Info.chargeTime;
        float currentValue = currentAmount * charge.Info.chargeTime + currentChargeTime;
        processing.TotalValue = totalValue;
        processing.CurrentValue = currentValue;
    }

    private void HandleStopCharge()
    {
        processing.Hide();
    }


    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        backgroundSprite = Resources.Load<Sprite>(Info.backgroundSprite);
        handleSprite = Resources.Load<Sprite>(Info.handleSprite);
        InitInfo();
    }

    private void InitInfo()
    {
        if (processing)
        {
            if (backgroundSprite)
                processing.BackgroundSpriteRenderer.sprite = backgroundSprite;
            if (handleSprite)
                processing.HandleSpriteRenderer.sprite = handleSprite;
        }
    }

    private void OnDestroy()
    {
        if (charge != null)
        {
            charge.OnStartCharge += HandleStartCharge;
            charge.OnChargeUpdate += HandleChargeUpdate;
            charge.OnStopCharge += HandleStopCharge;
        }
    }
}

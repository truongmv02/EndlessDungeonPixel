using UnityEngine;
using System;

public class WeaponChargeState : State
{
    WeaponController weapon;
    Charge charge;
    BaseStat chargeTime;
    BaseStat chargeAmount;

    public override void Enter()
    {
        base.Enter();
        charge.StartCharge();
    }

    public override void Exit()
    {
        base.Exit();
        charge.StopCharge();
        foreach (var condition in conditions)
        {
            condition.ResetCondition();
        }
    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        weapon = stateMachine.Owner as WeaponController;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponChargeState), animator.name);

        charge = weapon.GetBaseComponent<Charge>();
        BaseUtils.ValidateCheckNullValue(charge, nameof(charge), nameof(WeaponChargeState), animator.name);

        chargeTime = Stats["ChargeTime"];
        if (chargeTime != null)
        {
            HandChargeTimeChange(chargeTime.Value);
            chargeTime.OnValueChange += HandChargeTimeChange;
        }

        chargeAmount = Stats["ChargeAmount"];
        if (chargeAmount != null)
        {
            HandChargeAmountChange(chargeAmount.Value);
            chargeAmount.OnValueChange += HandChargeAmountChange;
        }
    }

    public void HandChargeTimeChange(float value)
    {
        charge.Info.chargeTime = value;
    }
    public void HandChargeAmountChange(float value)
    {
        charge.Info.chargeAmount = (int)value;
    }

    private void OnDestroy()
    {
        if (chargeTime != null)
        {
            chargeTime.OnValueChange -= HandChargeTimeChange;
        }

        if (chargeAmount != null)
        {
            chargeAmount.OnValueChange -= HandChargeAmountChange;
        }
    }
}

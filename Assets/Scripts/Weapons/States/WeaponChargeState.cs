using UnityEngine;
using System;

public class WeaponChargeState : State
{
    Charge charge;
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
        WeaponController weapon = stateMachine.Owner as WeaponController;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponChargeState), animator.name);
        charge = weapon.GetComponent<Charge>();
        BaseUtils.ValidateCheckNullValue(charge, nameof(charge), nameof(WeaponChargeState), animator.name);

        var chargeTime = stats["ChargeTime"];
        if (chargeTime != null)
        {
            charge.ChargeTime = chargeTime;
        }

        var chargeAmount = stats["ChargeAmount"];
        if (chargeAmount != null)
        {
            charge.ChargeAmount = chargeAmount;
        }
    }
}

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
    }

    public override void Init(StateMachine stateMachine, Animator animator)
    {
        base.Init(stateMachine, animator);
        WeaponController weapon = stateMachine.Owner as WeaponController;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponChargeState), animator.name);
        charge = weapon.GetComponent<Charge>();
        BaseUtils.ValidateCheckNullValue(charge, nameof(charge), nameof(WeaponChargeState), animator.name);
    }
}

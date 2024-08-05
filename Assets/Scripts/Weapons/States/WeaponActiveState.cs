using System.Collections;
using UnityEngine;

public class WeaponActiveState : State
{
    protected WeaponController weapon;

    protected void UseEnergy()
    {
        var ownerEnergy = weapon.Owner.Stats["CurrentEnergy"];
        if (ownerEnergy != null)
        {
            ownerEnergy.Value -= weapon.Stats["Energy"].Value;
        }
    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        weapon = (WeaponController)stateMachine.Owner;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponShootState), animator.name);
    }
}

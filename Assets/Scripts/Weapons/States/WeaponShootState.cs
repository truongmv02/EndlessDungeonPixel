using UnityEngine;

public class WeaponShootState : State
{
    protected WeaponController weapon;

    AddObjectHandle addObjectHandle;

    public override void Enter()
    {
        base.Enter();
        addObjectHandle.Position = weapon.AttackPoint.position;
        addObjectHandle.Direction = weapon.AttackPoint.right;
        addObjectHandle.Handle();
    }

    public override void Exit()
    {
        base.Exit();

        foreach (var condition in conditions)
        {
            condition.ResetCondition();
        }
    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        weapon = (WeaponController)stateMachine.Owner;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponShootState), animator.name);
        addObjectHandle = weapon.GetBaseComponent<AddObjectHandle>();
        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(WeaponShootState), animator.name);
    }
}

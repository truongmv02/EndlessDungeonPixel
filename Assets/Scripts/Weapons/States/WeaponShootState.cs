using UnityEngine;

public class WeaponShootState : State
{
    protected WeaponController weapon;

    AddObjectHandle addObjectHandle;

    public override void Enter()
    {
        base.Enter();
        addObjectHandle.Position = weapon.AttackPoint.position;
        addObjectHandle.Direction = weapon.transform.right;
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

    public override void Init(StateMachine stateMachine, Animator animator)
    {
        base.Init(stateMachine, animator);
        weapon = (WeaponController)stateMachine.Owner;
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(WeaponShootState), animator.name);
        addObjectHandle = weapon.GetComponent<AddObjectHandle>();
        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(WeaponShootState), animator.name);
    }
}

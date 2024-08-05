using UnityEngine;

public class WeaponShootState : WeaponActiveState
{

    AddObjectHandle addObjectHandle;

    public override void Enter()
    {
        base.Enter();
        addObjectHandle.Position = weapon.AttackPoint.position;
        addObjectHandle.Direction = weapon.AttackPoint.right;
        addObjectHandle.Handle();
        if (weapon.Info.sound)
            SoundManager.Instance.PlaySound(weapon.Info.sound);
        UseEnergy();
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
        addObjectHandle = weapon.GetHandle<AddObjectHandle>();
        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(WeaponShootState), animator.name);
    }
}

using UnityEngine;

public class WeaponShootState : State
{
    protected WeaponController weapon;

    public override void Enter()
    {
        base.Enter();
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
    }
}

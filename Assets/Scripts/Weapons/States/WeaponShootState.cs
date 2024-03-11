using UnityEngine;

public class WeaponShootState : State
{
    protected WeaponController weapon;

    AddObjectHandle addObject;

    public override void Enter()
    {
        base.Enter();
        addObject.Position = weapon.AttackPoint.position;
        addObject.Direction = weapon.transform.right;
        addObject.Handle();
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
        addObject = weapon.GetComponent<AddObjectHandle>();
    }
}

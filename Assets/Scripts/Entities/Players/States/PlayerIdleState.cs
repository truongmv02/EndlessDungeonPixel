using System.Collections;
using UnityEngine;

public class PlayerIdleState : State
{
    MoveController movement;

    public override void Enter()
    {
        base.Enter();
        movement.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movement.Stop();
    }

    public override void Init(StateMachine stateMachine, Animator animator)
    {
        base.Init(stateMachine, animator);
        PlayerController player = stateMachine.Owner as PlayerController;
        movement = player.Movement;
    }
}

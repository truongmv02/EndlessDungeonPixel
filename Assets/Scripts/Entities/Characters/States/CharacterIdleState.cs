using System.Collections;
using UnityEngine;

public class CharacterIdleState : State
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

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        CharacterController character = stateMachine.Owner as CharacterController;
        BaseUtils.ValidateCheckNullValue(character, nameof(character), nameof(CharacterIdleState), animator.name);
        movement = character.Movement;
    }
}



using UnityEngine;

public class CharacterRunState : State
{
    MoveController movement;
    CharacterController character;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (character == null || character.Control == null) return;
        Vector2 moveDirection = character.Control.InputHandler.MoveInput;
        movement.Move(moveDirection);

    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        character = stateMachine.Owner as CharacterController;
        BaseUtils.ValidateCheckNullValue(character, nameof(character), nameof(CharacterRunState), animator.name);
        movement = character.Movement;
    }
}
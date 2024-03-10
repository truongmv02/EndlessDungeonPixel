

using UnityEngine;

public class PlayerRunState : State
{
    MoveController movement;


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Move(moveDirection);
    }

    public override void Init(StateMachine stateMachine, Animator animator)
    {
        base.Init(stateMachine, animator);
        PlayerController player = stateMachine.Owner as PlayerController;
        movement = player.Movement;
    }
}
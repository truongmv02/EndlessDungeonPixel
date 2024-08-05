using System.Collections;
using UnityEngine;

public class EnemyAppearState : State
{
    EnemyController enemy;

    public override void Enter()
    {
        base.Enter();
        enemy.Movement.CanMove = false;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Movement.CanMove = true;
    }
    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        enemy = stateMachine.Owner as EnemyController;
    }
}

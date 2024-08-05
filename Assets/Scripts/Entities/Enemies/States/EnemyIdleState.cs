using System.Collections;
using UnityEngine;

public class EnemyIdleState : State
{
    EnemyController enemy;
    State runState;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var target = enemy.TargetDetector.GetTarget();
        if (target == null)
        {
            enemy.StateMachine.ChangeState(runState);
            return;
        }
        var distance = Vector2.Distance(target.transform.position, enemy.transform.position);
        if (distance > 1.5f)
        {
            enemy.StateMachine.ChangeState(runState);
        }
    }


    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        enemy = stateMachine.Owner as EnemyController;
        runState = stateMachine.GetState("Run");
    }
}

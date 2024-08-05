using System.Collections;
using UnityEngine;

public class EnemyRunIdleState : State
{
    float time;
    EnemyController enemy;
    State runState;
    public override void Enter()
    {
        base.Enter();
        time = Random.Range(1.5f, 2.5f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + time)
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

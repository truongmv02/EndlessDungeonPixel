using System.Collections;
using UnityEngine;

public class EnemyMeleeAttackState : State
{
    EnemyController enemy;



    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        enemy = stateMachine.Owner as EnemyController;
    }
}

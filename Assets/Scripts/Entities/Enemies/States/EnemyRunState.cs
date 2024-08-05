using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType
{
    MoveToTarget,
    MoveToOriginalPosition,
    MoveRandom
}


public class EnemyRunState : State
{
    EnemyController enemy;
    float destinationRandomTime;
    float lastChangeDirectionTime = 0;
    LayerMask wallMask;
    Vector2 destination;
    MoveType moveType = MoveType.MoveRandom;

    State runIdleState;
    State idleState;

    public override void Enter()
    {
        base.Enter();
        destinationRandomTime = Random.Range(3f, 5f);
        lastChangeDirectionTime = 0;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Move();

    }
    void Move()
    {
        var target = enemy.TargetDetector.GetTarget();
        var distanceCurrentPositionToOriginalPosition = Vector2.Distance(enemy.OriginalPosition, enemy.transform.position);
        if (target != null)
        {
            moveType = MoveType.MoveToTarget;
            enemy.Agent.speed = enemy.Stats["Speed"].Value;
        }
        else
        {
            moveType = MoveType.MoveRandom;
            if (distanceCurrentPositionToOriginalPosition >= 4f)
            {
                moveType = MoveType.MoveToOriginalPosition;
                enemy.Agent.speed = enemy.Stats["Speed"].Value;
            }
            if (distanceCurrentPositionToOriginalPosition < 0.1f)
            {
                moveType = MoveType.MoveRandom;
                enemy.Agent.speed = 0.8f;
            }

        }

        switch (moveType)
        {
            case MoveType.MoveToTarget:
            RunToTarget(target);
            break;

            case MoveType.MoveRandom:
            RunRandom();
            break;

            case MoveType.MoveToOriginalPosition:
            destination = enemy.OriginalPosition;
            enemy.Agent.SetDestination(destination);
            break;
        }
        enemy.Movement.CheckIfShouldFlip((destination - (Vector2)enemy.transform.position).x);
    }
    void RunToTarget(Transform target)
    {
        if (Vector2.Distance(target.transform.position, enemy.transform.position) > 1.5f)
            destination = target.position;
        else
        {
            destination = enemy.transform.position;
            enemy.StateMachine.ChangeState(idleState);
        }
        enemy.Agent.SetDestination(destination);
    }

    void RunRandom()
    {
        var hit = Physics2D.Raycast(enemy.transform.position, (destination - (Vector2)enemy.transform.position).normalized, Vector2.Distance(enemy.transform.position, destination), wallMask);

        if (lastChangeDirectionTime == 0f)
        {
            lastChangeDirectionTime = Time.time;
            destination = RandomDestination();
        }

        if (hit)
        {
            destination = RandomDestination();
        }

        if (Time.time >= lastChangeDirectionTime + destinationRandomTime)
        {
            enemy.StateMachine.ChangeState(runIdleState);
            destination = enemy.transform.position;
        }
        enemy.Agent.SetDestination(destination);
    }
    Vector2 RandomDestination()
    {
        Vector3 direction = Vector3.zero;
        direction.x = enemy.transform.position.x < enemy.OriginalPosition.x ? Random.Range(0, 1f) : Random.Range(-1, 0);
        direction.y = enemy.transform.position.y < enemy.OriginalPosition.y ? Random.Range(0, 1f) : Random.Range(-1, 0);
        var destination = enemy.transform.position + direction * 4;
        return destination;
    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        enemy = stateMachine.Owner as EnemyController;
        runIdleState = stateMachine.GetState("RunIdle");
        idleState = stateMachine.GetState("Idle");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StateInfo
{
    public string stateName;
    public string animationName;
    public string[] nextStates;
    public bool hasExit;
    public SubInfo[] conditions;
}

public class State : ISetInfo, IComparable<State>
{
    public int StateID { get; protected set; }
    public int AnimationID { get; protected set; }
    public StateInfo StateInfo { get; protected set; }
    public float StartTime { protected set; get; }
    protected float animationDuration;
    protected int order;

    public StateMachine StateMachine { set; get; }
    protected Animator animator;
    public List<State> NextStates { set; get; } = new List<State>();
    public List<ICondition> conditions = new List<ICondition>();

    public void SetInfo(object info)
    {
        StateInfo = info as StateInfo;
        StateID = Animator.StringToHash(StateInfo.stateName);
        AnimationID = Animator.StringToHash(StateInfo.animationName);
    }
    public virtual void Init(StateMachine stateMachine, Animator animator)
    {
        this.animator = animator;
        StateMachine = stateMachine;

        UtilsData.AddTypes(StateInfo.conditions, conditions, stateMachine.gameObject, (obj) =>
        {
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(stateMachine.Owner);
        });

        NextStates = StateMachine.GetNextStates(this);
        NextStates.Sort();
        var animationClip = animator.runtimeAnimatorController.animationClips.ToList().Find(x => x.name == StateInfo.stateName);
        animationDuration = animationClip.length;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;
        animator.Play(StateID);
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
        if (StateInfo.hasExit)
        {
            if (Time.time - StartTime > animationDuration)
            {
                StateMachine.ChangeState(NextStates[0]);
            }
        }
        else
        {
            foreach (var nextState in NextStates)
            {
                if (nextState.IsSuitable())
                {
                    StateMachine.ChangeState(nextState);
                    break;
                }
            }
        }
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual bool IsSuitable()
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsSuitable) return false;
        }

        return true;
    }

    public int CompareTo(State obj)
    {
        return order.CompareTo(obj.order);
    }
}

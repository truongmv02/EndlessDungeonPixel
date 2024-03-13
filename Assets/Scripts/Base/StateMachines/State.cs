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

public class State : MonoBehaviour, ISetInfo
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
    public virtual void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        this.animator = animator;
        StateMachine = stateMachine;

        UtilsData.AddTypes(StateInfo.conditions, conditions, gameObject, (obj) =>
        {
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(stateMachine.Owner);
            var countDownCondition = obj as CountDownCondition;
            if (countDownCondition != null)
            {
                BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(State) + " Init Method", name);
                countDownCondition.Cooldown = stats["Cooldown"];
                BaseUtils.ValidateCheckNullValue(countDownCondition.Cooldown, nameof(countDownCondition.Cooldown), nameof(State) + " Init Method", name);

            }
        });

        NextStates = StateMachine.GetNextStates(this);
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

}

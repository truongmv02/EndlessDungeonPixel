using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StateMachineInfo
{
    public SubInfo[] states;
}

public class StateMachine : MonoBehaviour
{
    public State CurrentStaste { set; get; }
    public List<State> StateList { get; set; } = new List<State>();
    public Animator Animator { get; set; }

    public object Owner { get; protected set; }


    private void Update()
    {
        CurrentStaste?.LogicUpdate();
    }
    private void FixedUpdate()
    {
        CurrentStaste?.PhysicsUpdate();
    }

    public void Init(object owner, StateMachineInfo info, Animator animator)
    {
        Owner = owner;
        Animator = animator;

        var states = transform.Find("States");
        if (states != null)
        {
            DestroyImmediate(states.gameObject);
        }

        var statesObj = new GameObject("States");
        statesObj.transform.SetParent(transform);

        UtilsData.AddStates(info.states, StateList, statesObj.transform);

        State idleState = null;

        foreach (var state in StateList)
        {
            state.Init(this, animator);
            if (state.StateInfo.stateName == "Idle") idleState = state;
        }
        ChangeState(idleState);
    }



    public void ChangeState(State state)
    {
        CurrentStaste?.Exit();
        CurrentStaste = state;
        state.Enter();
    }

    public List<State> GetNextStates(State state)
    {
        var list = new List<State>();
        foreach (var stateName in state.StateInfo.nextStates)
        {
            foreach (var s in StateList)
            {
                if (s.StateInfo.stateName == stateName)
                {
                    list.Add(s);
                }
            }
        }

        return list;

    }

    public State GetState(int stateID)
    {
        return StateList.Find(x => x.StateID == stateID);
    }

    public State GetState(Type type)
    {
        return StateList.Find(x => x.GetType() == type);
    }
}

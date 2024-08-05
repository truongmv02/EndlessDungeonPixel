using System;
using UnityEngine;

[Serializable]
public enum MovementState
{
    Run,
    Stop
}

public class MovementConditionInfo
{
    public string state;
}

public class MovementCondition : Condition<MovementConditionInfo>, ISetOwner
{
    protected MovementState state;
    protected CharacterController character;
    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        state = Enum.Parse<MovementState>(Info.state);
    }

    public void SetOwner(object owner)
    {
        character = owner as CharacterController;
    }

    protected override void CheckCondition()
    {
        if (character == null || character.Control == null) return;
        Vector2 direction = character.Control.InputHandler.MoveInput;

        bool isStop = direction.Equals(Vector2.zero);

        if (state == MovementState.Run)
        {
            SuitableCondition(!isStop);
        }
        else
        {
            SuitableCondition(isStop);
        }

    }
}


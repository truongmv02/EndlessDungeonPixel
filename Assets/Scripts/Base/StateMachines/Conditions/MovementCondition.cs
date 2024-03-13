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

public class MovementCondition : Condition<MovementConditionInfo>
{
    protected MovementState state;

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        state = Enum.Parse<MovementState>(Info.state);
    }


    protected override void CheckCondition()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);

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


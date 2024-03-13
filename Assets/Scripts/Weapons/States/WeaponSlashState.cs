using System.Collections;
using UnityEngine;

public class WeaponSlashState : State
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        foreach (var condition in conditions)
        {
            condition.ResetCondition();

        }
    }

}

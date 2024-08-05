using System.Collections;
using UnityEngine;

public class WeaponSlashState : WeaponActiveState
{
    public override void Enter()
    {
        base.Enter();
        UseEnergy();
        foreach (var condition in conditions)
        {
            condition.ResetCondition();
        }
    }
    public override void Exit()
    {
        base.Exit();


    }

}

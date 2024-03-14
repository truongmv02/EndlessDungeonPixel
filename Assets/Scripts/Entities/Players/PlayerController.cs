using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public class PlayerInfo
{

}

public class PlayerController : EntityController
{
    WeaponController weapon;

    protected override void Awake()
    {
        base.Awake();
        weapon = GetComponentInChildren<WeaponController>();

        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(PlayerController), name);

        Stats.Init(DataManager.Instance.PlayerStats.GetStats("ArthurPendragonStats"));
        Movement.Speed = Stats["Speed"];
        var stateMachineInfo = DataManager.Instance.PlayerStateMachine.GetInfo("base");
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);
    }

    protected override void Update()
    {
        base.Update();
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var playerDirection = mousePosition - transform.position;
        Movement.CheckIfShouldFlip(playerDirection.x);

        float radians = Mathf.Atan2(playerDirection.y, playerDirection.x);
        float degrees = radians * Mathf.Rad2Deg;
        float dir = playerDirection.x >= 0f ? 1f : -1f;
        float x = (playerDirection.x < 0f) ? 180 : 0;
        weapon.Root.localEulerAngles = new Vector3(x, x, degrees * dir);

        weapon.Input = Input.GetMouseButton(0);


    }
}

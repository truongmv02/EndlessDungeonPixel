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
        var stateMachineInfo = DataManager.Instance.StateMachineDatas.GetInfo(new[] { "Players", "base" });
        StateMachine.Init(this, stateMachineInfo, Animator);
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
        weapon.transform.localEulerAngles = new Vector3(x, x, degrees * dir);

        weapon.Input = Input.GetMouseButton(0);

    }
}

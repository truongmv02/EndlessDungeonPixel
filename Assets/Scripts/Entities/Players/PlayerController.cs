using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Data.SqlTypes;

[Serializable]
public class PlayerInfo
{

}

public class PlayerController : EntityController
{
    Inventory inventory;
    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
        BaseUtils.ValidateCheckNullValue(inventory, nameof(inventory), nameof(PlayerController), name);

        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo("PlayerDetector"));
        Stats.Init(DataManager.Instance.PlayerStats.GetStats("ArthurPendragonStats"));
        Movement.Speed = Stats["Speed"];
        var stateMachineInfo = DataManager.Instance.PlayerStateMachine.GetInfo("base");
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TargetDetector.GetTarget()?.GetComponent<ItemPickupController>()?.Collect(this);
        }
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var playerDirection = mousePosition - transform.position;
        Movement.CheckIfShouldFlip(playerDirection.x);

        float radians = Mathf.Atan2(playerDirection.y, playerDirection.x);
        float degrees = radians * Mathf.Rad2Deg;
        float dir = playerDirection.x >= 0f ? 1f : -1f;
        float x = (playerDirection.x < 0f) ? 180 : 0;
        inventory.Weapon.Root.localEulerAngles = new Vector3(x, x, degrees * dir);

        inventory.Weapon.Input = Input.GetMouseButton(0);
    }

}

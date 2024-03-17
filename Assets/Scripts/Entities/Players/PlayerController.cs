using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Data.SqlTypes;

[Serializable]
public class PlayerInfo : EntityInfo
{

}

public class PlayerController : EntityController
{
    PlayerInfo Info;
    Inventory inventory;

    public void SetInfo(PlayerInfo info)
    {
        Info = info;

        Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Info.runtimeAnimatorController);

        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo(Info.detectorInfo));

        Stats.Init(DataManager.Instance.PlayerStats.GetStats(Info.stats));

        var stateMachineInfo = DataManager.Instance.PlayerStateMachine.GetInfo(Info.stateMachine);
        BaseUtils.ValidateCheckNullValue(stateMachineInfo, nameof(stateMachineInfo), nameof(PlayerController));
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);
    }

    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
        BaseUtils.ValidateCheckNullValue(inventory, nameof(inventory), nameof(PlayerController), name);
        Name = "ArthurPendragon";
        SetInfo(DataManager.Instance.PlayerData.GetInfo(Name));
        Movement.Speed = Stats["Speed"];
    }
    private void Start()
    {
        GameManager.Instance.Player = this;
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

using System;
using UnityEngine;

[Serializable]
public class CharacterInfo : EntityInfo
{
    public Sprite sprite;
    public WeaponInfo initialWeapon;
    public int[] prices;
}

public class CharacterController : EntityController
{
    public CharacterInfo Info { set; get; }
    public Inventory Inventory { get; protected set; }
    public PlayerController Control { set; get; }

    public void SetInfo(CharacterInfo info)
    {
        Info = info;
        Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Info.runtimeAnimatorController);

        InitStat(DataManager.Instance.CharacterStats, info);

        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo(Info.detectorInfo));

        var stateMachineInfo = DataManager.Instance.CharacterStateMachine.GetInfo(Info.stateMachine);
        BaseUtils.ValidateCheckNullValue(stateMachineInfo, nameof(stateMachineInfo), nameof(CharacterController));
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);

        Inventory.AddWeapon(info.initialWeapon);
        Inventory.Weapon.SetInfo(info.initialWeapon);
        Inventory.Weapon.Owner = this;
        /* var weapons = DataManager.Instance.WeaponData.GetAllInfo();
         foreach (var weapon in weapons)
         {
             Inventory.AddWeapon(weapon);
         }*/
    }

    protected override void Awake()
    {
        base.Awake();
        Inventory = GetComponent<Inventory>();
        Inventory.Owner = this;
        BaseUtils.ValidateCheckNullValue(Inventory, nameof(Inventory), nameof(CharacterController), name);
    }

    public override void InitStat(StatDatas statDatas, EntityInfo info)
    {
        base.InitStat(statDatas, info);


        BaseStat energy = Stats["Energy"];
        BaseStat currentEnergy = Stats["CurrentEnergy"];

        if (currentEnergy == null)
        {
            currentEnergy = new BaseStat() { StatName = "CurrentEnergy", Value = energy.Value };
        }
        Stats[currentEnergy.StatName] = currentEnergy;
    }

    private void Start()
    {
        InvokeRepeating("RecoveryEnergy", 30f, 30f);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void RecoveryEnergy()
    {
        BaseStat currentEnergy = Stats["CurrentEnergy"];
        if (currentEnergy != null)
            Stats.IncrementCurrentStat("CurrentEnergy", 15f, Stats);
    }

    public override void Death()
    {
        base.Death();
        CancelInvoke("RecoveryEnergy");
        Observer.Instance.Notify(ConstanVariable.PLAYER_DIE_KEY, this);
    }

}

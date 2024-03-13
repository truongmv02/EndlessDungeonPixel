using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    public Vector2 position;
    public Vector2 attackPosition;
    public float rotation;
    public RuntimeAnimatorController runtimeAnimatorController;
    public string stats;
    public string stateMachine;
    public SubInfo[] components;
    public SubInfo[] conditions;
}

[DisallowMultipleComponent]
public class WeaponController : RootComponent<WeaponInfo>, IGetInput
{
    public string weaponName;
    public int level;
    public StateMachine StateMachine { get; protected set; }
    public bool Input { protected get; set; }
    public Transform AttackPoint { set; get; }
    public SpriteRenderer WeaponSprite { get; set; }
    public SpriteRenderer OptionalSprite { get; protected set; }
    public EntityController Owner { get; set; }
    public Animator Animator { get; protected set; }
    public WeaponAnimationEventHandler AnimationHandler { get; protected set; }
    public Stats Stats { get; protected set; } = new Stats();
    public Transform Root { get; protected set; }
    public Transform WeaponRotation { get; protected set; }

    [ContextMenu("Init Weapon")]
    public void Init()
    {
        SetInfo(DataManager.Instance.WeaponDatas.GetInfo(weaponName));
    }
    [ContextMenu("Level Up")]
    public void LevelUp()
    {
        Stats.Init(DataManager.Instance.WeaponStats.GetStats(Info.stats, level));
    }

    public bool GetInput()
    {
        return Input;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        OptionalSprite.enabled = false;
        Root.transform.localPosition = Info.position;
        Animator.runtimeAnimatorController = Info.runtimeAnimatorController;
        WeaponSprite.transform.localEulerAngles = new Vector3(0f, 0f, Info.rotation);
        AttackPoint.localPosition = Info.attackPosition;

        Stats.Clear();
        Stats.Init(DataManager.Instance.WeaponStats.GetStats(Info.stats));

        UtilsData.AddTypes(Info.components, components, gameObject, (comp) =>
        {
            var setOwner = comp as ISetOwner;
            setOwner?.SetOwner(this);
            var setStats = comp as ISetStats;
            setStats?.SetStats(Stats);
        });

        StateMachineInfo stateInfo = DataManager.Instance.WeaponStateMachine.GetInfo(Info.stateMachine);
        StateMachine.Init(this, stateInfo, Animator, Stats);
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StateMachine = GetComponentInChildren<StateMachine>();
        AnimationHandler = GetComponent<WeaponAnimationEventHandler>();
        Root = transform.Find("Root");
        WeaponRotation = Root.Find("WeaponRotation");
        WeaponSprite = WeaponRotation.Find("WeaponSprite").GetComponent<SpriteRenderer>();
        AttackPoint = WeaponRotation.Find("AttackPosition");
        OptionalSprite = GetComponentInChildren<OptionalSpriteMarket>().SpriteRenderer;

        BaseUtils.ValidateCheckNullValue(Animator, nameof(Animator), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(StateMachine, nameof(StateMachine), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(StateMachine, nameof(StateMachine), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(AnimationHandler, nameof(AnimationHandler), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(OptionalSprite, nameof(OptionalSprite), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(AttackPoint, nameof(AttackPoint), nameof(WeaponController), name);

        SetInfo(DataManager.Instance.WeaponDatas.GetInfo("Sword"));
    }

}

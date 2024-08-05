using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointPolygon2D
{
    public Vector2[] points;
}

[Serializable]
public class WeaponInfo : BaseInfo
{
    public Vector2 position;
    public float rotation;
    public bool isActiveHitbox;
    public Vector2 attackPosition;
    public float attackPointRotation;
    public string stats;
    public string stateMachine;
    public PointPolygon2D hitbox;
    public RuntimeAnimatorController runtimeAnimatorController;
    public AudioClip sound;
    public SubInfo[] components;
    public SubInfo[] conditions;
    public SubInfo[] handles;
}

[DisallowMultipleComponent]
public class WeaponController : RootComponent<WeaponInfo>, IGetInput
{
    public int level;
    public Color color;
    public StateMachine StateMachine { get; protected set; }
    public bool InputValue { protected get; set; }
    public Transform AttackPoint { set; get; }
    public SpriteRenderer WeaponSprite { get; set; }
    public SpriteRenderer OptionalSprite { get; protected set; }
    public EntityController Owner { get; set; }
    public Animator Animator { get; protected set; }
    public WeaponAnimationEventHandler AnimationHandler { get; protected set; }
    public Stats Stats { get; protected set; }
    public Transform Root { get; protected set; }
    public Transform WeaponRotation { get; protected set; }
    PolygonCollider2D hitbox;

    [ContextMenu("Level Up")]
    public void LevelUp()
    {
        Stats.Init(DataManager.Instance.WeaponStats.GetStats(Info.stats, level));
    }

    public bool GetInput()
    {
        return InputValue;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        OptionalSprite.enabled = false;
        Root.transform.localPosition = Info.position;
        Animator.runtimeAnimatorController = Info.runtimeAnimatorController;
        WeaponSprite.transform.localEulerAngles = new Vector3(0f, 0f, Info.rotation);
        AttackPoint.localPosition = Info.attackPosition;
        AttackPoint.localEulerAngles = new Vector3(0f, 0f, Info.attackPointRotation);
        hitbox.enabled = Info.isActiveHitbox;
        hitbox.points = Info.hitbox?.points;

        Stats.Clear();
        Stats.Init(DataManager.Instance.WeaponStats.GetStats(Info.stats));

        UtilsData.AddTypes(Info.components, components, gameObject, (comp) =>
        {
            var setOwner = comp as ISetOwner;
            setOwner?.SetOwner(this);
            var setStats = comp as ISetStats;
            setStats?.SetStats(Stats);
        });

        UtilsData.AddTypes(Info.handles, handles, gameObject, (comp) =>
        {
            var setOwner = comp as ISetOwner;
            setOwner?.SetOwner(this);
            var setStats = comp as ISetStats;
            setStats?.SetStats(Stats);
        });

        UtilsData.AddTypes(Info.conditions, conditions, gameObject, (comp) =>
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
        hitbox = AttackPoint.GetComponentInChildren<PolygonCollider2D>();
        Stats = GetComponent<Stats>();
        // Debug.Log(JsonUtility.ToJson(new PointPolygon2D() { points = hitbox.points }));
        // Debug.Log(JsonUtility.ToJson(color));

        BaseUtils.ValidateCheckNullValue(Animator, nameof(Animator), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(StateMachine, nameof(StateMachine), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(AnimationHandler, nameof(AnimationHandler), nameof(WeaponController), name);

        BaseUtils.ValidateCheckNullValue(Root, nameof(Root), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(WeaponRotation, nameof(WeaponRotation), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(WeaponSprite, nameof(WeaponSprite), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(AttackPoint, nameof(AttackPoint), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(OptionalSprite, nameof(OptionalSprite), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(hitbox, nameof(hitbox), nameof(WeaponController), name);
        BaseUtils.ValidateCheckNullValue(Stats, nameof(Stats), nameof(WeaponController), name);

    }

}

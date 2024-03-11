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
    public SubInfo[] components;
    public SubInfo[] conditions;
}

[DisallowMultipleComponent]
public class WeaponController : RootComponent<WeaponInfo>, IGetInput
{
    public StateMachine StateMachine { get; protected set; }
    public bool Input { protected get; set; }
    public Transform AttackPoint { set; get; }
    public SpriteRenderer WeaponSprite { get; set; }
    public EntityController Owner { get; set; }
    public Animator Animator { get; protected set; }
    public WeaponAnimationEventHandler AnimationHandler { get; protected set; }

    public bool GetInput()
    {
        return Input;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        transform.localPosition = Info.position;
        Animator.runtimeAnimatorController = Info.runtimeAnimatorController;
        WeaponSprite.transform.localEulerAngles = new Vector3(0f, 0f, Info.rotation);
        AttackPoint.localPosition = Info.attackPosition;
        UtilsData.AddTypes(Info.components, components, gameObject, (comp) =>
        {
            var setOwner = comp as ISetOwner;
            setOwner?.SetOwner(this);
        });
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StateMachine = GetComponentInChildren<StateMachine>();
        AnimationHandler = GetComponent<WeaponAnimationEventHandler>();
        WeaponSprite = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();
        AttackPoint = transform.Find("AttackPosition");
        SetInfo(DataManager.Instance.WeaponDatas.GetInfo("Pistol"));
        StateMachineInfo stateInfo = DataManager.Instance.StateMachineDatas.GetInfo(new[] { "Weapons", "Pistol" });
        StateMachine.Init(this, stateInfo, Animator);
    }

}

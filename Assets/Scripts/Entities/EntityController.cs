using System.Collections;
using UnityEngine;

[System.Serializable]
public class EntityInfo
{
    public string prefab;
    public string runtimeAnimatorController;
    public string stats;
    public string stateMachine;
    public string detectorInfo;
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EntityController : MonoBehaviour
{
    [SerializeField] public string Name { set; get; }
    public Animator Animator { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public MoveController Movement { get; protected set; }
    public StateMachine StateMachine { get; protected set; }
    public Stats Stats { get; protected set; }
    public TargetDetector TargetDetector { get; protected set; }

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Movement = GetComponent<MoveController>();
        StateMachine = GetComponentInChildren<StateMachine>();
        Stats = GetComponent<Stats>();
        TargetDetector = GetComponentInChildren<TargetDetector>();

        BaseUtils.ValidateCheckNullValue(Animator, nameof(Animator), nameof(EntityController), name);
        BaseUtils.ValidateCheckNullValue(SpriteRenderer, nameof(SpriteRenderer), nameof(EntityController), name);
        BaseUtils.ValidateCheckNullValue(Rigidbody, nameof(Rigidbody), nameof(EntityController), name);
        BaseUtils.ValidateCheckNullValue(Movement, nameof(Movement), nameof(EntityController), name);
        BaseUtils.ValidateCheckNullValue(StateMachine, nameof(StateMachine), nameof(EntityController), name);
        BaseUtils.ValidateCheckNullValue(Stats, nameof(Stats), nameof(EntityController), name);
    }

    protected virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}

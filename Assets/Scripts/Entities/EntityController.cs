using System.Collections;
using UnityEngine;

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
    public Stats Stats { get; protected set; } = new Stats();

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Movement = GetComponent<MoveController>();
        StateMachine = GetComponentInChildren<StateMachine>();
    }

    protected virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}

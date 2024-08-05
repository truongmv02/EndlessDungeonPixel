using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public enum ItemPickupType
{
    None,
    AutoPickup
}

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ItemPickupController : MonoBehaviour, ISetInfo
{
    public BoxCollider2D BoxCollider2D { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    protected EntityController entity;
    float moveSpeed = 20f;
    public ItemPickupType Type { set; get; }

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public abstract void Collect(EntityController entity);

    public void SetTargetCollect(EntityController target)
    {
        if (entity == null)
        {
            entity = target;
        }
    }


    protected virtual void Update()
    {
        Move();
    }

    void Move()
    {
        if (entity != null)
        {
            var distance = Vector2.Distance(transform.position, entity.transform.position);

            if (distance < 0.2f)
            {
                Collect(entity);
                Destroy(gameObject);
            }
            else
            {
                var direction = entity.transform.position - transform.position;
                Rigidbody2D.velocity = direction.normalized * moveSpeed;
            }
        }
    }

    public virtual void SetInfo(object info)
    {

    }
}
public abstract class ItemPickupController<T> : ItemPickupController where T : BaseInfo
{
    [field: SerializeField] public T Info { protected set; get; }
    public override void SetInfo(object info)
    {
        Info = (T)(info as BaseInfo);
        SpriteRenderer.sprite = Info.sprite;
        BoxCollider2D.size = Info.sprite.bounds.size;
    }
}

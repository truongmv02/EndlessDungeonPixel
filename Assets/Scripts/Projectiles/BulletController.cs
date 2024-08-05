using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ProjectileController, ISetStats
{
    SpriteRenderer spriteRenderer;
    BaseStat range;
    BaseStat speed;
    MoveController movement;
    public Stats Stats { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<MoveController>();
    }


    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(transform.position, StartPosition) >= range.Value)
        {
            Disable();
        }
    }
    protected override void HandleCollision(List<Collider2D> colliders)
    {
        if (combatAction.SetInactiveAfterCollision)
        {
            Disable();
        }
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        spriteRenderer.enabled = Info.sprite == null ? false : true;
        var material = Resources.Load<Material>(Info.material);
        if (material != null)
        {
            spriteRenderer.material = material;
        }
    }

    private void Disable()
    {
        if (Info.hitEffect != null)
            ObjectPool.Instance.GetObject(Info.hitEffect.prefab, transform.position, Vector2.up, Info.hitEffect);
        ObjectPool.Instance.DestroyObject(gameObject);
    }

    public void SetStats(Stats stats)
    {
        range = stats["Range"];
        speed = stats["BulletSpeed"];
        movement.Speed = speed;
    }


}

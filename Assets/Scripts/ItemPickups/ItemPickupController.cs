using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickupController : MonoBehaviour, ISetInfo
{

    public BoxCollider2D BoxCollider2D { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public BaseInfo Info { get; protected set; }
    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Collect(EntityController entity)
    {
    }

    public void SetInfo(object info)
    {
        Info = info as BaseInfo;
        SpriteRenderer.sprite = Info.sprite;
    }
}

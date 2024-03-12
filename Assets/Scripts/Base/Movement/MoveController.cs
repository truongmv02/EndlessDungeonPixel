using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    // public float Speed { set; get; } = 4;
    public BaseStat Speed { set; get; } = new BaseStat() { Value = 4 };
    public bool CanMove { get; set; }

    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity => _rigidbody.velocity;


    protected virtual void Awake()
    {
        CanMove = true;
        FacingDirection = 1;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
    }

    public virtual void Move(Vector2 direction)
    {
        Move(Speed.Value, direction);
    }
    public virtual void Move(float speed, Vector2 direction)
    {
        direction = direction.normalized;
        SetFinalVelocity(direction * speed);
    }

    public virtual void Stop()
    {
        SetFinalVelocity(Vector2.zero);
    }

    protected virtual void SetFinalVelocity(Vector2 velocity)
    {
        if (!CanMove) return;
        _rigidbody.velocity = velocity;
    }

    public void CheckIfShouldFlip(float xdirection)
    {
        if (Mathf.Abs(xdirection) < 0.01f || xdirection * FacingDirection >= 0) return;
        Flip();
    }

    private void Flip()
    {
        FacingDirection *= -1;
        _rigidbody.transform.Rotate(0f, 180, 0f);
    }
}

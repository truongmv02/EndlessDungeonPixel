using System.Collections;
using UnityEngine;
public interface ITypeMove
{
    Vector2 Move();
}

[DisallowMultipleComponent]
public class TypeMoveController : MoveController
{
    ITypeMove[] typeMoves;

    protected override void Start()
    {
        base.Start();
        typeMoves = GetComponents<ITypeMove>();
    }

    private void Update()
    {
        if (!CanMove) return;

        Vector2 direction = Vector2.zero;

        foreach (var typeMove in typeMoves)
        {
            direction += typeMove.Move();
        }
        _rigidbody.transform.up = direction;
        Move(direction);
    }
}

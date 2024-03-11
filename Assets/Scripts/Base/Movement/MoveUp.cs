using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveUp : MonoBehaviour, ITypeMove
{
    public Vector2 Move()
    {
        return transform.up;
    }
}

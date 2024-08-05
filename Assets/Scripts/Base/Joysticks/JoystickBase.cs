using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoystickBase : MonoBehaviour
{
    public Image BackgroundImage { set; get; }
    public Image HandleImage { set; get; }
    public Vector2 Direction { protected set; get; }
    public Vector2 LastDirection { protected set; get; }
    public Vector3 OriginalPosition { set; get; }

    public event Action<Vector2> OnJoystickBegan;
    public event Action<Vector2> OnJoystickMove;
    public event Action<Vector2> OnJoystickEnded;

    protected float radius;

    protected virtual void Awake()
    {
        BackgroundImage = GetComponent<Image>();
        HandleImage = transform.Find("Handle").GetComponent<Image>();
        radius = BackgroundImage.rectTransform.sizeDelta.x / 2f;
        OriginalPosition = transform.position;
    }

    protected virtual void Start() { }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void JoystickBegan(Vector2 position)
    {
        MoveJoystick(position);
        OnJoystickBegan?.Invoke(position);
    }

    protected virtual void JoystickMove(Vector2 position)
    {
        MoveJoystick(position);
        OnJoystickMove?.Invoke(position);
    }

    protected virtual void JoystickEnded(Vector2 position)
    {
        Direction = Vector2.zero;
        transform.position = OriginalPosition;
        HandleImage.transform.localPosition = Vector2.zero;

        OnJoystickEnded?.Invoke(position);
    }

    protected void MoveJoystick(Vector2 position)
    {
        Vector2 offset = position - (Vector2)transform.position;
        Vector3 realDirection = Vector2.ClampMagnitude(offset, radius);
        Direction = realDirection.normalized;
        LastDirection = Direction;
        Vector2 handPos = new(transform.position.x + realDirection.x, transform.position.y + realDirection.y);
        HandleImage.transform.position = handPos;
    }
}
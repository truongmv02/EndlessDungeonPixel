using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class OnScreenMoveStick : OnScreenControl
{
    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    public float MovementRange { get; set; }

    public void MoveStick(Vector2 pointerPosition)
    {
        var offset = pointerPosition - (Vector2)transform.parent.position;
        var delta = Vector2.ClampMagnitude(offset, MovementRange);
        var newPos = new Vector2(delta.x / MovementRange, delta.y / MovementRange);
        SendValueToControl(newPos);
    }

    public void Stop()
    {
        SendValueToControl(Vector2.zero);
    }
}

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class MovementJoystick : JoystickBase
{
    OnScreenMoveStick moveStick;
    Finger movementFinger;
    protected override void Awake()
    {
        base.Awake();
        moveStick = GetComponentInChildren<OnScreenMoveStick>();
        moveStick.MovementRange = radius;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ETouch.EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerMove += HandleFingerMove;
        ETouch.Touch.onFingerUp += HandleFingerUp;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.EnhancedTouchSupport.Disable();
    }

    private void HandleFingerUp(Finger finger)
    {
        if (finger != movementFinger) return;
        JoystickEnded(finger.currentTouch.screenPosition);
        moveStick.Stop();
    }

    private void HandleFingerMove(Finger finger)
    {
        if (finger != movementFinger) return;
        Vector2 position = finger.currentTouch.screenPosition;
        JoystickMove(position);
        moveStick.MoveStick(position);
    }

    private void HandleFingerDown(Finger finger)
    {
        if (finger.screenPosition.x < Screen.width * 0.45f)
        {
            movementFinger = finger;
            var position = ClampStartPosition(finger.currentTouch.screenPosition);
            transform.position = position;
            JoystickBegan(position);
            moveStick.MoveStick(position);
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < radius)
        {
            startPosition.x = radius;
        }
        if (startPosition.y < radius)
        {
            startPosition.y = radius;
        }
        else if (startPosition.y > Screen.height - radius)
        {
            startPosition.y = Screen.height - radius;
        }
        return startPosition;
    }

}

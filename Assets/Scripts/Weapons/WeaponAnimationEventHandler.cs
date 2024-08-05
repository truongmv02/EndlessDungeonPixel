using System;
using System.Collections;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    public event Action<bool> OnSetActiveOptionalSprite;
    public event Action OnResetConditions;

    public void ResetConditions() => OnResetConditions?.Invoke();
    public void SetOptionalSpriteEnable() { OnSetActiveOptionalSprite?.Invoke(true); }
    public void SetOptionalSpriteDisable() { OnSetActiveOptionalSprite?.Invoke(false); }
}

using System;
using System.Collections;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    public event Action<bool> OnSetActiveOptionalSprite;
    public void SetOptionalSpriteEnable() { OnSetActiveOptionalSprite?.Invoke(true); }
    public void SetOptionalSpriteDisable() { OnSetActiveOptionalSprite?.Invoke(false); }
}

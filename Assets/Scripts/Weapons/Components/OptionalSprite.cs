using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class OptionalSpriteInfo
{
    public string sprite;
    public Vector2 postion;
    public float rotation;
}

public class OptionalSprite : BaseComponent<OptionalSpriteInfo>, ISetOwner
{
    WeaponController weapon;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        BaseUtils.ValidateCheckNullValue(weapon, nameof(weapon), nameof(OptionalSprite), name);
        spriteRenderer = weapon.OptionalSprite;
        spriteRenderer.sprite = Resources.Load<Sprite>(Info.sprite);
        spriteRenderer.transform.localPosition = Info.postion;
        spriteRenderer.transform.localEulerAngles = new Vector3(0f, 0f, Info.rotation);
        weapon.AnimationHandler.OnSetActiveOptionalSprite += HandleSetActiveOptionalSprite;

    }

    public void HandleSetActiveOptionalSprite(bool value)
    {
        spriteRenderer.enabled = value;
    }


    public void SetOwner(object owner)
    {
        weapon = owner as WeaponController;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);

    }

    private void OnDestroy()
    {
        // weapon.AnimationHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
    }
}

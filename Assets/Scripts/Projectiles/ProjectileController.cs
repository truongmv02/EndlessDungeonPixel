using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProjectileInfo
{
    public string prefab;
    public string material;
    public Sprite sprite;
    public ProjectileHitEffectSO hitEffect;
    public SubInfo[] components;
}


[DisallowMultipleComponent]
public class ProjectileController : RootComponent<ProjectileInfo>, IResetObject
{
    public Vector2 StartPosition { set; get; }


    protected CombatAction combatAction;

    protected virtual void Awake()
    {
        combatAction = GetComponent<CombatAction>();
        combatAction.OnCollision += HandleCollision;
    }

    protected virtual void Update()
    {

    }

    public virtual void ResetObject()
    {
        StartPosition = transform.position;
    }
    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        UtilsData.AddTypes(Info.components, components, gameObject);
    }

    protected virtual void HandleCollision(List<Collider2D> colliders)
    {
    }

    private void OnDestroy()
    {
        if (combatAction != null)
        {
            combatAction.OnCollision -= HandleCollision;
        }
    }

}

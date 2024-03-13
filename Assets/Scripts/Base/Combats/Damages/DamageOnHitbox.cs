using System;
using System.Linq;
using UnityEngine;

public class DamageOnHitbox : Damage, ISetInfo
{
    public event Action OnDamage;
    [field: SerializeField] public CombatInfo Info { get; set; }
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision.transform);
    }


    void Damage(Transform target)
    {
        if (!target.TryGetComponent<IDamageable>(out var damageable) || damage == null) return;
        if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
        {
            damageInfo.amount = damage.Value;
            damageable.Damage(damageInfo);
        }
        OnDamage?.Invoke();
    }

    public void SetInfo(object info)
    {
        Info = info as CombatInfo;
    }

    public void HandleStatChange(float value)
    {
        damageInfo.amount = value;
    }
}

using System;
using System.Linq;
using UnityEngine;

public class DamageOnHitbox : MonoBehaviour, ISetInfo, ISetDamageInfo
{
    [field: SerializeField] public CombatInfo Info { get; set; }
    public event Action OnDamage;
    DamageInfo damageInfo;
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision.transform);
    }


    void Damage(Transform target)
    {
        if (!target.TryGetComponent<IDamageable>(out var damageable)) return;
        if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
        {
            damageable.Damage(damageInfo);
        }
        OnDamage?.Invoke();
    }

    public void SetDamageInfo(DamageInfo info)
    {
        damageInfo = info;
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

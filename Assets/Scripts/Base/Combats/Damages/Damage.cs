
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damage : Combat, ISetInfo
{
    public event Action OnDamage;

    protected BaseStat damage;
    protected DamageInfo damageInfo = new DamageInfo();

    protected override void Start()
    {
        base.Start();
        combatAction.OnCollision += HandleCollision;
    }

    void HandleCollision(List<Collider2D> colliders)
    {
        if (!CanCombat()) return;
        foreach (var collider in colliders)
        {
            var target = collider.gameObject;
            if (!target.TryGetComponent<IDamageable>(out var damageable) || damage == null) continue;
            if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
            {
                lastCombatTime = Time.time;
                damageInfo.amount = damage.Value;
                damageable.Damage(damageInfo);
                OnDamage?.Invoke();
            }
        }
    }

    public void SetInfo(object info)
    {
        Info = info as CombatInfo;
    }

    public override void SetStats(Stats stats)
    {
        base.SetStats(stats);
        damage = stats["Damage"];
        BaseUtils.ValidateCheckNullValue(damage, nameof(damage), nameof(Damage), name);
    }


    private void OnDestroy()
    {
        if (combatAction != null)
        {
            combatAction.OnCollision -= HandleCollision;
        }
    }
}

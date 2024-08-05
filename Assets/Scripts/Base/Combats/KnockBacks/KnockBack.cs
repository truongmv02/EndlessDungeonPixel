using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnockBack : Combat, ISetInfo
{
    public event Action OnKnockBack;

    protected BaseStat knockBackStrength;
    protected KnockBackInfo knockBackInfo = new KnockBackInfo();
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
            var target = collider.transform;
            if (!target.TryGetComponent<IKnockBackable>(out var knockBack) || knockBackStrength == null) continue;
            if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
            {
                lastCombatTime = Time.time;
                knockBackInfo.strength = knockBackStrength.Value;
                knockBackInfo.direction = transform.up;
                knockBack.KnockBack(knockBackInfo);
                OnKnockBack?.Invoke();
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
        knockBackStrength = stats["KnockBackStrength"];
        BaseUtils.ValidateCheckNullValue(knockBackStrength, nameof(knockBackStrength), nameof(KnockBack), name);
    }

    private void OnDestroy()
    {
        if (combatAction != null)
        {
            combatAction.OnCollision -= HandleCollision;
        }
    }

}

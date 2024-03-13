using System;
using System.Linq;
using UnityEngine;

public class KnockBackOnHitbox : KnockBack, ISetInfo
{
    public event Action OnKnockBack;
    [field: SerializeField] public CombatInfo Info { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KnockBack(collision.transform);
    }

    void KnockBack(Transform target)
    {
        if (!target.TryGetComponent<IKnockBackable>(out var knockBack) || knockBackStrength == null) return;
        if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
        {
            knockBackInfo.strength = knockBackStrength.Value;
            knockBackInfo.direction = (target.position - transform.position).normalized;
            knockBack.KnockBack(knockBackInfo);
        }
        OnKnockBack?.Invoke();
    }

    public void SetInfo(object info)
    {
        Info = info as CombatInfo;
    }


}

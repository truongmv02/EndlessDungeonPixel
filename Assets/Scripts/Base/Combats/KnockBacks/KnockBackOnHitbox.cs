using System;
using System.Linq;
using UnityEngine;

public class KnockBackOnHitbox : MonoBehaviour, ISetInfo, ISetKnockBackInfo
{
    [field: SerializeField] public CombatInfo Info { get; set; }
    public event Action OnKnockBack;
    KnockBackInfo knockBackInfo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        KnockBack(collision.transform);
    }

    void KnockBack(Transform target)
    {
        if (!target.TryGetComponent<IKnockBackable>(out var knockBack)) return;
        if (!string.IsNullOrEmpty(Info.objectTags.FirstOrDefault(x => x == target.tag)))
        {
            knockBackInfo.direction = transform.up;
            knockBack.KnockBack(knockBackInfo);
        }
        OnKnockBack?.Invoke();
    }

    public void SetKnockBackInfo(KnockBackInfo info)
    {
        knockBackInfo = info;
    }

    public void SetInfo(object info)
    {
        Info = info as CombatInfo;
    }
}

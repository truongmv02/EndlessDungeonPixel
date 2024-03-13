using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour, ISetStats
{
    protected Stats stats;
    protected BaseStat knockBackStrength;
    protected KnockBackInfo knockBackInfo = new KnockBackInfo();
    public virtual void SetStats(Stats stats)
    {
        this.stats = stats;
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(KnockBackOnHitbox), name);
        knockBackStrength = stats["KnockBackStrength"];
        BaseUtils.ValidateCheckNullValue(knockBackStrength, nameof(knockBackStrength), nameof(KnockBackOnHitbox), name);
    }
}


using UnityEngine;

public abstract class Damage : MonoBehaviour, ISetStats
{
    protected Stats stats;
    protected BaseStat damage;
    protected DamageInfo damageInfo = new DamageInfo();
    public virtual void SetStats(Stats stats)
    {
        this.stats = stats;
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(Damage), name);
        damage = stats["Damage"];
        BaseUtils.ValidateCheckNullValue(damage, nameof(damage), nameof(Damage), name);
    }
}

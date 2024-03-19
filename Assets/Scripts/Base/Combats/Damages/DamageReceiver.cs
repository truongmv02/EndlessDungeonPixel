using UnityEngine;

[DisallowMultipleComponent]
public class DamageReceiver : MonoBehaviour, IDamageable
{
    Stats stats;
    BaseStat health;
    private void Start()
    {
        stats = GetComponentInParent<Stats>();
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(DamageReceiver), transform.parent.name);
        health = stats["Health"];
        BaseUtils.ValidateCheckNullValue(health, nameof(health), nameof(DamageReceiver), transform.parent.name);
    }

    public void Damage(DamageInfo damageInfo)
    {
        health.Value -= damageInfo.amount;
    }
}
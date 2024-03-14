using System.Collections;
using UnityEngine;

public class WeaponDamage : MonoBehaviour, ISetStats
{
    DamageOnHitbox damageOnHitbox;
    Stats stats;
    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }

    void Start()
    {
        damageOnHitbox = GetComponentInChildren<DamageOnHitbox>();
        damageOnHitbox.SetStats(stats);
    }

}

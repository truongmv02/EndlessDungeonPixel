using System.Collections;
using UnityEngine;

public class WeaponKnockBack : MonoBehaviour, ISetStats
{
    KnockBackOnHitbox damageOnHitbox;
    Stats stats;
    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }

    void Start()
    {
        damageOnHitbox = GetComponentInChildren<KnockBackOnHitbox>();
        damageOnHitbox.SetStats(stats);
    }

}

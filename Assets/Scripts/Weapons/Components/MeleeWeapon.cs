using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, ISetStats
{
    Stats stats;
    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }

    void Start()
    {
        var combats = GetComponentsInChildren<Combat>();
        foreach (var combat in combats)
        {
            combat.SetStats(stats);
        }
    }
}

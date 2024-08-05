
using UnityEngine;

[System.Serializable]
public class CombatInfo
{
    public string[] objectTags;
}

public abstract class Combat : MonoBehaviour, ISetStats
{
    [field: SerializeField] public CombatInfo Info { set; get; }
    protected Stats stats;
    protected float lastCombatTime = -Mathf.Infinity;
    protected BaseStat cooldown;
    protected CombatAction combatAction;

    protected virtual void Start()
    {
        combatAction = GetComponent<CombatAction>();
    }

    public virtual void SetStats(Stats stats)
    {
        this.stats = stats;
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(Damage), name);
        cooldown = stats["DamageCooldown"];

    }

    protected bool CanCombat()
    {
        if (cooldown == null || Time.time >= lastCombatTime + cooldown.Value) return true;
        return false;
    }
    private void OnDisable()
    {
        lastCombatTime = -Mathf.Infinity;
    }
}

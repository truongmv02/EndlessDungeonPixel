using System.Collections;
using UnityEngine;

[System.Serializable]
public class ItemBuffInfo : BaseInfo
{
    public StatInfos statInfos;
}


public class ItemBuffPickupController : ItemPickupController<ItemBuffInfo>
{
    TargetDetector targetDetector;

    private void Start()
    {
        Type = ItemPickupType.AutoPickup;
        targetDetector = GetComponent<TargetDetector>();
    }

    protected override void Update()
    {
        if (targetDetector.GetTarget() != null && entity == null)
        {
            entity = targetDetector.GetTarget().GetComponent<CharacterController>();
        }
        base.Update();
    }



    public override void Collect(EntityController entity)
    {
        foreach (var stat in Info.statInfos.stats)
        {
            Stats.IncrementCurrentStat(stat.statName, stat.value, entity.Stats);
        }
    }
}

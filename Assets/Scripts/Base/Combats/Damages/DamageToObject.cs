using System.Collections;
using UnityEngine;

public class DamageToObject : MonoBehaviour, ISetStats
{
    DamageInfo damageInfo = new DamageInfo();
    AddObjectHandle addObjectHandle;
    Stats stats;
    BaseStat damage;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        damage = stats["Damage"];

        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(DamageToObject), name);
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(DamageToObject), name);
        BaseUtils.ValidateCheckNullValue(damage, nameof(damage), nameof(DamageToObject), name);

        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }

    private void HandleCreateObjectFinish(GameObject obj)
    {
        ISetDamageInfo setDamage = obj.GetComponent<ISetDamageInfo>();
        damageInfo.amount = damage.Value;
        setDamage?.SetDamageInfo(damageInfo);
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
}

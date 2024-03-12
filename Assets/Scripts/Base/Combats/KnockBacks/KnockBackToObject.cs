using System;
using System.Collections;
using UnityEngine;

public class KnockBackToObject : MonoBehaviour, ISetStats
{
    KnockBackInfo knockBackInfo = new KnockBackInfo();
    AddObjectHandle addObjectHandle;
    Stats stats;
    BaseStat strength;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        strength = stats["KnockBackStrength"];

        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(KnockBackToObject), name);
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(KnockBackToObject), name);
        BaseUtils.ValidateCheckNullValue(strength, nameof(strength), nameof(KnockBackToObject), name);

        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }
    private void HandleCreateObjectFinish(GameObject obj)
    {
        ISetKnockBackInfo setKnockBack = obj.GetComponent<ISetKnockBackInfo>();
        knockBackInfo.strength = strength.Value;
        setKnockBack?.SetKnockBackInfo(knockBackInfo);
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
}

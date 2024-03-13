using System;
using System.Collections;
using UnityEngine;

public class KnockBackToObject : MonoBehaviour, ISetStats
{
    Stats stats;
    AddObjectHandle addObjectHandle;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();

        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(KnockBackToObject), name);
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(KnockBackToObject), name);

        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }
    private void HandleCreateObjectFinish(GameObject obj)
    {
        KnockBack knockBack = obj.GetComponent<KnockBack>();
        knockBack?.SetStats(stats);
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
}

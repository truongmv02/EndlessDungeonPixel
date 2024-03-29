﻿using System.Collections;
using UnityEngine;

public class DamageToObject : MonoBehaviour, ISetStats
{
    AddObjectHandle addObjectHandle;
    Stats stats;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }

    private void HandleCreateObjectFinish(GameObject obj)
    {
        Damage setStats = obj.GetComponent<Damage>();
        setStats?.SetStats(stats);
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
}

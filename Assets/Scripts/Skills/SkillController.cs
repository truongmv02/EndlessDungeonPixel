using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

[Serializable]
public class SkillInfoDetails
{
    public string skillName;
    public string skillStats;
}

[Serializable]
public class SkillInfo
{
    public string prefab;
    public SubInfo[] components;
    public SubInfo[] conditions;
    public SubInfo[] handles;
}

public class SkillController : RootComponent<SkillInfo>
{
    public bool Input { get; set; }
    public EntityController Owner { get; set; }
    public Stats Stats { set; get; }

    private void Awake()
    {
        Stats = GetComponent<Stats>();
    }



    public override void SetInfo(object info)
    {
        base.SetInfo(info);

        UtilsData.AddTypes(Info.components, components, gameObject, (obj) =>
        {
            var setStats = obj as ISetStats;
            setStats?.SetStats(Stats);
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
        });
        UtilsData.AddTypes(Info.handles, handles, gameObject, (obj) =>
        {
            var setStats = obj as ISetStats;
            setStats?.SetStats(Stats);
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
        });
        UtilsData.AddTypes(Info.conditions, conditions, gameObject, (obj) =>
        {
            var setStats = obj as ISetStats;
            setStats?.SetStats(Stats);
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
            var condition = obj as ICondition;
            // condition.OnSuitable = OnSuitable;
        });

    }

    public void Handle()
    {
        foreach (var cond in conditions)
        {
            cond.ResetCondition();
        }
        foreach (var handle in handles)
        {
            handle.Handle();
        }
    }

    void OnSuitable(ICondition condition)
    {
        if (!CheckConditions()) return;
        foreach (var cond in conditions)
        {
            cond.ResetCondition();
        }
        foreach (var handle in handles)
        {
            handle.Handle();
        }
    }

    public bool CheckConditions()
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsSuitable) return false;
        }

        return true;
    }

    public virtual void InitStat(StatDatas statDatas, string path)
    {
        var statInfos = statDatas.GetStats(path);
        Stats.Init(statInfos);
    }

}

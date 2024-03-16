using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void SetInfo(object info)
    {
        base.SetInfo(info);

        UtilsData.AddTypes(Info.components, components, gameObject, (obj) =>
        {
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
        });
        UtilsData.AddTypes(Info.handles, handles, gameObject, (obj) =>
        {
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
        });
        UtilsData.AddTypes(Info.conditions, conditions, gameObject, (obj) =>
        {
            var setOwner = obj as ISetOwner;
            setOwner?.SetOwner(this);
            var condition = obj as ICondition;
            condition.OnSuitable = OnSuitable;
        });

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

    bool CheckConditions()
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsSuitable) return false;
        }

        return true;
    }


}

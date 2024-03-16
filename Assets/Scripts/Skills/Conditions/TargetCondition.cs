using System.Collections;
using UnityEngine;

public class TargetCondition : Condition, ISetOwner
{
    SkillController skill;
    AddObjectHandle addObjectHandle;
    Transform target;
    private void Awake()
    {
        isSuitable = false;
    }

    private void Update()
    {
        target = skill.Owner.TargetDetector.GetTarget();
        if (addObjectHandle && target)
        {
            addObjectHandle.Direction = (target.position - transform.position).normalized;
            addObjectHandle.Position = transform.position;
        }
        SuitableCondition(target != null);
    }

    public void SetOwner(object owner)
    {
        skill = (SkillController)owner;
        addObjectHandle = skill.GetHandle<AddObjectHandle>();
    }

    public override void ResetCondition()
    {
    }

    private void OnDestroy()
    {
    }
}

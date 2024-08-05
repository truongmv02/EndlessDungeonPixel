using System;
using System.Collections;
using UnityEngine;

public abstract class Condition : MonoBehaviour, ICondition
{
    protected bool isSuitable = true;
    public virtual bool IsSuitable
    {
        get
        {
            CheckCondition();
            return isSuitable;
        }
    }

    protected Action<ICondition> onSuitable;
    public Action<ICondition> OnSuitable { set => onSuitable = value; }

    protected void SuitableCondition(bool isSuitable)
    {
        if (this.isSuitable == isSuitable) return;

        this.isSuitable = isSuitable;
        if (onSuitable != null)
        {
            onSuitable(this);
        }
    }

    public virtual void ResetCondition()
    {
        isSuitable = false;
    }

    protected virtual void CheckCondition()
    {

    }
}


public abstract class Condition<T> : Condition, ISetInfo where T : class, new()
{
    protected T Info { set; get; } = new T();
    public virtual void SetInfo(object info)
    {
        Info = (T)info;
    }
}
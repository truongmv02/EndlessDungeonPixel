using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootComponent<T> : BaseComponent<T> where T : class, new()
{

    protected List<Component> components = new List<Component>();
    protected List<IHandle> handles = new List<IHandle>();
    protected List<ICondition> conditions = new List<ICondition>();


    public virtual void AddComponent(SubInfo[] subInfos)
    {

    }

    public virtual T1 GetBaseComponent<T1>() where T1 : Component
    {
        foreach (var comp in components)
        {
            if (comp as T1)
            {
                return comp as T1;
            }
        }
        return null;
    }
    public virtual T1 GetHandle<T1>() where T1 : class, IHandle
    {
        foreach (var handle in handles)
        {
            if (handle as T1 != null)
            {
                return handle as T1;
            }
        }
        return null;
    }

    public virtual T1 GetCondition<T1>() where T1 : class, ICondition
    {
        foreach (var condition in conditions)
        {
            if (condition as T1 != null)
            {
                return condition as T1;
            }
        }
        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootComponent<T> : BaseComponent<T> where T : class, new()
{

    protected List<Component> components = new List<Component>();
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
}

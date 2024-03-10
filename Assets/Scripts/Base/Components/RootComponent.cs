using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootComponent<T> : BaseComponent<T>
{

    protected List<Component> components = new List<Component>();
    public virtual void AddComponent(SubInfo[] subInfos)
    {

    }
}

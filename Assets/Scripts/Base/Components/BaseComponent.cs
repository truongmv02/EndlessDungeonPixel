using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent<T> : MonoBehaviour, ISetInfo where T : class, new()
{
    [field: SerializeField] public virtual T Info { set; get; } = new T();
    public virtual void SetInfo(object info)
    {
        Info = (T)info;
    }
}

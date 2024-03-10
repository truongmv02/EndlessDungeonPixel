using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent<T> : MonoBehaviour, ISetInfo
{
    public virtual T Info { set; get; }
    public virtual void SetInfo(object info)
    {
        Info = (T)info;
    }
}

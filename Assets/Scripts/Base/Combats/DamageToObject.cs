using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AddObjectHandle))]
public class DamageToObject : BaseComponent<DamageInfo>
{
    AddObjectHandle addObjectHandle;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        addObjectHandle.CreateObjCallback = (obj) =>
        {
            ISetDamageInfo setDamage = obj.GetComponent<ISetDamageInfo>();
            setDamage?.SetDamageInfo(Info);
        };
    }
}

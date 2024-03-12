using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AddObjectHandle))]
public class KnockBackToObject : BaseComponent<KnockBackInfo>
{
    AddObjectHandle addObjectHandle;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }

    private void HandleCreateObjectFinish(GameObject obj)
    {
        ISetKnockBackInfo setKnockBack = obj.GetComponent<ISetKnockBackInfo>();
        setKnockBack?.SetKnockBackInfo(Info);
    }
}

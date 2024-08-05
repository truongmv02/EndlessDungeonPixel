using System.Collections;
using UnityEngine;

public class CombatRange : MonoBehaviour, ISetStats
{
    Stats stats;
    AddObjectHandle addObjectHandle;
    private void Start()
    {
        addObjectHandle = GetComponent<AddObjectHandle>();
        addObjectHandle.OnCreateObjectFinish += HandleCreateObjectFinish;
    }

    private void OnDestroy()
    {
        if (addObjectHandle != null)
            addObjectHandle.OnCreateObjectFinish -= HandleCreateObjectFinish;
    }
    private void HandleCreateObjectFinish(GameObject obj)
    {
        var setStats = obj.GetComponents<ISetStats>();
        foreach (var setStat in setStats)
        {
            setStat?.SetStats(stats);
        }
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
}

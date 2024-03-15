using System.Collections;
using UnityEngine;

public class CheckLowestHealth : MonoBehaviour, ICheckBestTarget
{
    public Transform CheckBestTarget(Transform bestTarget, Transform targetToCheck)
    {
        if (bestTarget == null) return targetToCheck;

        var bestStats = bestTarget.parent?.GetComponent<Stats>();
        var stats = targetToCheck.parent?.GetComponent<Stats>();

        if (bestStats == null || stats == null) return null;

        var bestHealth = bestStats["Health"];
        var health = stats["Health"];

        if (bestHealth == null || health == null) return null;

        if (bestHealth.Value == health.Value) return null;

        if (health.Value < bestHealth.Value) return bestTarget;

        return targetToCheck;

    }
}

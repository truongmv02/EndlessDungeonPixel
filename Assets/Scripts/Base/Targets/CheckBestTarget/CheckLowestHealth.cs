using System.Collections;
using UnityEngine;

public class CheckLowestHealth : MonoBehaviour, ICheckBestTarget
{
    public Transform CheckBestTarget(Transform bestTarget, Transform targetToCheck)
    {
        if (bestTarget == null) return targetToCheck;

        var bestStats = bestTarget?.GetComponent<Stats>();
        var stats = targetToCheck?.GetComponent<Stats>();

        if (bestStats == null || stats == null)
        {
            Debug.Log("Stats null");
            return null;
        }

        var bestHealth = bestStats["CurrentHealth"];
        var health = stats["CurrentHealth"];

        if (bestHealth == null || health == null)
        {
            Debug.Log("health null");
            return null;
        }

        if (bestHealth.Value == health.Value) return null;

        if (health.Value < bestHealth.Value) return targetToCheck;

        return bestTarget;

    }
}

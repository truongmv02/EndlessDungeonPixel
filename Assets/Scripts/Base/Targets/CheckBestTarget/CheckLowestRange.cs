using System.Collections;
using UnityEngine;

public class CheckLowestRange : MonoBehaviour, ICheckBestTarget
{
    public Transform CheckBestTarget(Transform bestTarget, Transform targetToCheck)
    {
        if (bestTarget == null) return targetToCheck;

        float bestRange = Vector2.Distance(transform.position, bestTarget.position);
        float range = Vector2.Distance(transform.position, targetToCheck.position);

        if (bestRange == range) return null;
        if (range < bestRange) return targetToCheck;
        return bestTarget;
    }

}

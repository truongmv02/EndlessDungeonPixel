using System.Collections;
using UnityEngine;

public class CheckObstacle : MonoBehaviour, ICheckTarget
{
    LayerMask layer;

    private void Start()
    {
        layer = LayerMaskHelper.MergeLayerMask(LayerMaskHelper.ObstacleMask, LayerMaskHelper.WallMask);
    }
    public bool CheckTarget(Transform target)
    {
        var direction = (target.position - transform.position).normalized;
        var distance = Vector2.Distance(target.position, transform.position);
        var hits = Physics2D.RaycastAll(transform.position, direction, distance, layer);
        Debug.DrawRay(transform.position, direction * distance, Color.red);
        return hits.Length == 0;
    }
}

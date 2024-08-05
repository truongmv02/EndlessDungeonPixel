using System.Collections;
using UnityEngine;

[System.Serializable]
public class LaserBeamInfo
{
    public string material;
    public float startWidth;
    public float endWidth;
    public float hitboxWidth;
    public Color startColor;
    public Color endColor;
}

public class LaserBeam : BaseComponent<LaserBeamInfo>, ISetStats
{
    LineRenderer lineRenderer;
    LayerMask obstacles;
    BoxCollider2D hitbox;
    BaseStat range;
    private void Awake()
    {
        obstacles = LayerMaskHelper.MergeLayerMask(LayerMaskHelper.ObstacleMask, LayerMaskHelper.WallMask);
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnDisable()
    {
        DrawLaser(Vector2.zero, Vector2.zero);
    }

    private void Update()
    {
        Vector2 direction = transform.up;
        var hit = Physics2D.Raycast(transform.position, direction, range.Value, obstacles);
        if (hit)
        {
            DrawLaser(transform.position, hit.point);
        }
        else
        {
            DrawLaser(transform.position, (Vector2)transform.position + direction * range.Value);
        }
    }

    private void DrawLaser(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
        float laserLength = Vector2.Distance(startPos, endPos);
        hitbox.size = new Vector2(Info.hitboxWidth, laserLength);
        hitbox.offset = new Vector2(0, laserLength / 2f);
    }
    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.startWidth = Info.startWidth;
        lineRenderer.endWidth = Info.endWidth;
        lineRenderer.startColor = Info.startColor;
        lineRenderer.endColor = Info.endColor.a == 0f ? Info.startColor : Info.endColor;
        lineRenderer.material = Resources.Load<Material>(Info.material);
    }

    public void SetStats(Stats stats)
    {
        range = stats["Range"];
    }
}

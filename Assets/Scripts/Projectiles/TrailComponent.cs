using System.Collections;
using UnityEngine;

[System.Serializable]
public class TrailInfo
{
    public string material;
    public float time;
    public float startWidth;
    public float endWidth;
}

public class TrailComponent : BaseComponent<TrailInfo>
{
    TrailRenderer trail;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        trail.time = 0;
    }


    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        trail = GetComponentInChildren<TrailRenderer>();
        trail.emitting = true;
        trail.startWidth = Info.startWidth;
        trail.endWidth = Info.endWidth;
        trail.time = Info.time;
        trail.material = Resources.Load<Material>(Info.material);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;

[System.Serializable]
public class TargetInfo
{
    public string targetTag;
    public float minRange;
    public float maxRange;
}

[System.Serializable]
public class TargetDetectorInfo
{
    public float detectDelay;
    public float detectRange;
    public string[] layers;
    public TargetInfo[] targetInfos;
}

public class TargetDetector : BaseComponent<TargetDetectorInfo>
{
    private Transform currentTarget;
    public List<Collider2D> Targets { protected set; get; }

    protected LayerMask layer;
    ICheckTarget[] checkTargets;
    ICheckBestTarget[] checkBestTargets;

    Coroutine detectCoroutine;
    WaitForSeconds waitForSeconds;

    public Action<Transform> OnChangeTarget;

    private void Start()
    {
        checkTargets = GetComponents<ICheckTarget>();
        checkBestTargets = GetComponents<ICheckBestTarget>();
        StartDetect();
    }
    public Transform GetTarget()
    {
        return currentTarget;
    }
    public void StartDetect()
    {
        detectCoroutine = StartCoroutine(DetectCoroutine());
    }

    public void StopDetect()
    {
        if (detectCoroutine == null) return;
        StopCoroutine(detectCoroutine);
        detectCoroutine = null;
    }

    private IEnumerator DetectCoroutine()
    {
        while (true)
        {
            Detect();
            yield return waitForSeconds;
        }
    }
    void Detect()
    {
        Targets = Physics2D.OverlapCircleAll(transform.position, Info.detectRange, layer).ToList<Collider2D>();
        FilterTarget();
    }

    public void FilterTarget()
    {
        Transform target = null;
        for (int i = Targets.Count - 1; i >= 0; i--)
        {
            if (!CheckTarget(Targets[i].transform))
            {
                Targets.RemoveAt(i);
                continue;
            }
            target = CheckBestTarget(target, Targets[i].transform);
        }
        if (target != currentTarget)
        {
            currentTarget = target;
            OnChangeTarget?.Invoke(currentTarget);
        }
    }

    bool CheckTarget(Transform target)
    {
        if (Info.targetInfos != null)
        {
            foreach (var targetInfo in Info.targetInfos)
            {
                if (target.tag == targetInfo.targetTag)
                {
                    if (targetInfo.maxRange == 0) break;
                    float distance = Vector2.Distance(target.position, transform.position);
                    if (distance < targetInfo.minRange || distance > targetInfo.maxRange) return false;
                    break;
                }
            }
        }
        foreach (var checkTarget in checkTargets)
        {
            if (!checkTarget.CheckTarget(target)) return false;
        }
        return true;
    }
    Transform CheckBestTarget(Transform bestTarget, Transform target)
    {
        if (checkBestTargets.Length == 0) return target;
        foreach (var checkBestTarget in checkBestTargets)
        {
            var result = checkBestTarget.CheckBestTarget(bestTarget, target);
            if (result != null) return result;
        }
        return bestTarget;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        layer = LayerMask.GetMask(Info.layers);
        waitForSeconds = new WaitForSeconds(Info.detectDelay);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Info.detectRange);
    }
}

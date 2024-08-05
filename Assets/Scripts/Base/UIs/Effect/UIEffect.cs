using System;
using UnityEngine;

public class UIEffect : MonoBehaviour
{
    public float animationTime = 0.2f;
    public GameObject root;

    public virtual void Appear()
    {

    }
    public virtual void Disappear()
    {
        if (root != null)
        {
            LeanTween.delayedCall(animationTime, () =>
            {
                root.SetActive(false);
            }).setIgnoreTimeScale(true);
        }
    }
}



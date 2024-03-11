using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BulletInfo
{
    public Sprite sprite;
    public SubInfo[] components;
}

[DisallowMultipleComponent]
public class BulletController : RootComponent<BulletInfo>, IResetObject
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //textMeshPro.text = a;
    }

    public void ResetObject()
    {

    }
}

using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public class PlayerInfo
{

}

public class PlayerController : EntityController
{
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Update()
    {
        base.Update();
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var playerDirection = mousePosition - transform.position;

    }
}

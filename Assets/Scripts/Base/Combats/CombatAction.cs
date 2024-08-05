using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAction : MonoBehaviour
{
    public event Action<List<Collider2D>> OnCollision;
    bool active;
    [field: SerializeField] public bool SetInactiveAfterCollision { set; get; } = true;
    [field: SerializeField] public bool DamageOnTriggerStay { set; get; } = false;
    Collider2D col;
    protected virtual void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    bool CanCollision()
    {
        if (!SetInactiveAfterCollision) return true;
        if (SetInactiveAfterCollision && !active) return true;
        return false;
    }

    private void OnEnable()
    {
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanCollision()) return;
        List<Collider2D> colliders = new List<Collider2D>();
        col.GetContacts(colliders);
        OnCollision?.Invoke(colliders);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!DamageOnTriggerStay) return;
        if (!CanCollision()) return;
        List<Collider2D> colliders = new List<Collider2D>();
        col.GetContacts(colliders);

        OnCollision?.Invoke(colliders);
    }

}

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class DamageReceiver : MonoBehaviour, IDamageable
{
    public void Damage(DamageInfo damageInfo)
    {
        Debug.Log(transform.parent.name + " receive damage: " + damageInfo.amount);
    }
}
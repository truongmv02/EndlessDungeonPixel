using UnityEngine;

[DisallowMultipleComponent]
public class DamageReceiver : MonoBehaviour, IDamageable
{
    public void Damage(DamageInfo damageInfo)
    {
        Debug.Log(transform.parent.name + " receive damage: " + damageInfo.amount);
    }
}
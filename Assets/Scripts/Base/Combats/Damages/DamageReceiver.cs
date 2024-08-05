using UnityEngine;

[DisallowMultipleComponent]
public class DamageReceiver : MonoBehaviour, IDamageable
{
    Stats stats;

    GameObject damagePopup;
    private void Start()
    {
        stats = GetComponentInParent<Stats>();
        BaseUtils.ValidateCheckNullValue(stats, nameof(stats), nameof(DamageReceiver), transform.parent.name);
        damagePopup = Resources.Load<GameObject>("Prefab/DamagePopup");
    }

    public void Damage(DamageInfo damageInfo)
    {
        var health = stats["CurrentHealth"];
        health.Value -= damageInfo.amount;

        var textPopup = ObjectPool.Instance.GetObject(damagePopup, transform.position + new Vector3(0, 0.5f, 0), Vector2.up).GetComponent<TextPopup>();
        textPopup.Setup("-" + damageInfo.amount.ToString("N0"));

        // Debug.Log(damageInfo.amount);
    }
}
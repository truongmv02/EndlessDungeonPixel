
[System.Serializable]
public class DamageInfo
{
    public float amount;
}

public interface IDamageable
{
    void Damage(DamageInfo damageInfo);
}
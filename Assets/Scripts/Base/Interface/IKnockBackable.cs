using UnityEngine;

public class KnockBackInfo
{
    public float strength;
    public Vector2 direction;
}

public interface IKnockBackable
{
    void KnockBack(KnockBackInfo knockBackInfo);
}
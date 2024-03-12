using UnityEngine;

public class KnockBackReceiver : MonoBehaviour, IKnockBackable
{
    public void KnockBack(KnockBackInfo knockBackInfo)
    {
        Debug.Log(transform.parent.name + " Receiver knock back strength " + knockBackInfo.strength + ", direction " + knockBackInfo.direction);
    }
}
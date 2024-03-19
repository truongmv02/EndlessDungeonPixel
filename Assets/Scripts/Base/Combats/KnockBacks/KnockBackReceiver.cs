using System;
using System.Collections;
using UnityEngine;

public class KnockBackReceiver : MonoBehaviour, IKnockBackable
{
    MoveController movement;
    float knockBackTime = 0.1f;

    WaitForSeconds waitForSeconds;
    private void Start()
    {
        movement = GetComponentInParent<MoveController>();
        waitForSeconds = new WaitForSeconds(knockBackTime);
    }
    public void KnockBack(KnockBackInfo knockBackInfo)
    {
        Debug.Log(knockBackInfo.strength);
        movement.Move(knockBackInfo.strength, knockBackInfo.direction);
        movement.CanMove = false;
        StartCoroutine(ResetKnockBack());
    }
    public IEnumerator ResetKnockBack()
    {
        yield return waitForSeconds;
        movement.CanMove = true;
        movement.Stop();
    }


}
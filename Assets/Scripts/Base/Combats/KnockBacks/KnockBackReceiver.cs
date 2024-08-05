using System;
using System.Collections;
using UnityEngine;

public class KnockBackReceiver : MonoBehaviour, IKnockBackable
{
    MoveController movement;
    float knockBackTime = 0.1f;

    WaitForSeconds waitForSeconds;
    Coroutine resetKnockBackCoroutine;
    private void Start()
    {
        movement = GetComponentInParent<MoveController>();
        waitForSeconds = new WaitForSeconds(knockBackTime);
    }
    public void KnockBack(KnockBackInfo knockBackInfo)
    {
        if (!movement.CanMove)
        {
            movement.CanMove = true;
            if (resetKnockBackCoroutine != null)
                StopCoroutine(resetKnockBackCoroutine);
        }
        movement.Move(knockBackInfo.strength, knockBackInfo.direction);
        movement.CanMove = false;
        resetKnockBackCoroutine = StartCoroutine(ResetKnockBack());
    }
    public IEnumerator ResetKnockBack()
    {
        yield return waitForSeconds;
        movement.CanMove = true;
        movement.Stop();
        resetKnockBackCoroutine = null;
    }


}
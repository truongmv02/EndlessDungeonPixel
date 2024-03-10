using UnityEngine;

[System.Serializable]
public class CountDownInfo
{
    public float timeCountDown;
    public bool startValue = false;
}
public class CountDownCondition : Condition<CountDownInfo>
{
    public override void ResetCondition()
    {
        base.ResetCondition();
        LeanTween.delayedCall(info.timeCountDown, () => SuitableCondition(true));
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        isSuitable = this.info.startValue;
    }
}

using UnityEngine;

[System.Serializable]
public class CountDownInfo
{
    public bool startValue = true;
}
public class CountDownCondition : Condition<CountDownInfo>, ISetStats
{
    public BaseStat Cooldown { get; set; }
    private float timer;
    private void Start()
    {
    }

    private void Update()
    {
        if (isSuitable) return;
        timer += Time.deltaTime;
        if (timer >= Cooldown.Value)
        {
            SuitableCondition(true);
            timer = 0;
        }

    }
    public override void ResetCondition()
    {
        base.ResetCondition();
        timer = 0;
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        isSuitable = this.info.startValue;
    }

    public void SetStats(Stats stats)
    {
        Cooldown = stats["Cooldown"];
        BaseUtils.ValidateCheckNullValue(Cooldown, nameof(Cooldown), nameof(CountDownCondition), name);
    }
}

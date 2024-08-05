using UnityEngine;

[System.Serializable]
public class CountDownInfo
{
    public float timeCountDown;
    public bool startValue = true;
}
public class CountDownCondition : Condition<CountDownInfo>, ISetStats
{
    private float timer;
    BaseStat cooldown;


    private void Start()
    {
    }

    private void OnEnable()
    {
        timer = 0;
        isSuitable = Info.startValue;
    }

    private void Update()
    {
        if (isSuitable) return;
        timer += Time.deltaTime;
        if (timer >= Info.timeCountDown)
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
        isSuitable = Info.startValue;
    }

    public void SetStats(Stats stats)
    {
        cooldown = stats["Cooldown"];
        if (cooldown != null)
        {
            HandleCooldownChange(cooldown.Value);
            cooldown.OnValueChange += HandleCooldownChange;
        }
    }

    private void HandleCooldownChange(float value)
    {
        Info.timeCountDown = value;
    }

    private void OnDestroy()
    {
        if (cooldown != null)
            cooldown.OnValueChange -= HandleCooldownChange;
    }
}

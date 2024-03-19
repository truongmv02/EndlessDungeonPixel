using System.Collections;
using UnityEngine;


[System.Serializable]
public class EnemyInfo : EntityInfo
{
    public string[] skills;

}

public class EnemyController : EntityController
{
    EnemyInfo Info;
    private Transform skillHolder;
    protected override void Awake()
    {
        base.Awake();
        skillHolder = transform.Find("Skills");
    }

    private void Start()
    {
        BaseStat health = Stats["Health"];
        health.OnValueZero += Death;

    }

    public void SetInfo(EnemyInfo info)
    {
        Info = info;
        Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Info.runtimeAnimatorController);
        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo(Info.detectorInfo));
        Stats.Init(DataManager.Instance.PlayerStats.GetStats(Info.stats));
        var stateMachineInfo = DataManager.Instance.EnemyStateMachine.GetInfo(Info.stateMachine);
        BaseUtils.ValidateCheckNullValue(stateMachineInfo, nameof(stateMachineInfo), nameof(EnemyController));
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);

        /*   foreach (var skillName in Info.skills)
           {
               SkillInfo skillInfo = DataManager.Instance.SkillData.GetInfo(skillName);
               GameObject skillPrefab = Resources.Load<GameObject>(skillInfo.prefab);
               SkillController skill = Instantiate(skillPrefab, skillHolder).GetComponent<SkillController>();
               skill.name = skillName;
               skill.SetInfo(skillInfo);
               skill.Owner = this;
           }*/
    }

    void Death()
    {
        Observer.Instance.Notify("EnemyDie", this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        BaseStat health = Stats["Health"];
        health.OnValueZero -= Death;
    }

}

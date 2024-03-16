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
        SetInfo(DataManager.Instance.EnemyData.GetInfo("Warlock"));
    }

    public void SetInfo(EnemyInfo info)
    {
        Info = info;
        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo(Info.detectorInfo));
        foreach (var skillName in Info.skills)
        {
            SkillInfo skillInfo = DataManager.Instance.SkillData.GetInfo(skillName);
            GameObject skillPrefab = Resources.Load<GameObject>(skillInfo.prefab);
            SkillController skill = Instantiate(skillPrefab, skillHolder).GetComponent<SkillController>();
            skill.name = skillName;
            skill.SetInfo(skillInfo);
            skill.Owner = this;
        }
    }

}

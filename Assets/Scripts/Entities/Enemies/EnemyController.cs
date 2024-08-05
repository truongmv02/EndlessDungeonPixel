using MVT.Base.Dungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : EntityController
{
    public EnemyInfo Info { set; get; }
    private Transform skillHolder;
    public RoomController Owner { set; get; }
    public Vector2 OriginalPosition { set; get; }
    public List<SkillController> Skills { protected set; get; } = new();

    public NavMeshAgent Agent { protected set; get; }


    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        skillHolder = transform.Find("Skills");
    }

    private void OnEnable()
    {
    }

    public override void SetInfo(object info)
    {
        Info = info as EnemyInfo;
        Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Info.runtimeAnimatorController);
        InitStat(DataManager.Instance.EnemyStats, Info);
        TargetDetector.SetInfo(DataManager.Instance.DetectorData.GetInfo(Info.detectorInfo));
        var stateMachineInfo = DataManager.Instance.EnemyStateMachine.GetInfo(Info.stateMachine);
        BaseUtils.ValidateCheckNullValue(stateMachineInfo, nameof(stateMachineInfo), nameof(EnemyController));
        StateMachine.Init(this, stateMachineInfo, Animator, Stats);
        foreach (var skill in Skills)
        {
            Destroy(skill.gameObject);
        }
        Skills.Clear();
        foreach (var skillDetail in Info.skills)
        {
            SkillInfo skillInfo = DataManager.Instance.EnemySkillData.GetInfo(skillDetail.skillName);
            GameObject skillPrefab = Resources.Load<GameObject>(skillInfo.prefab);
            SkillController skill = Instantiate(skillPrefab, skillHolder).GetComponent<SkillController>();
            skill.Owner = this;
            skill.InitStat(DataManager.Instance.EnemySkillStats, skillDetail.skillStats);
            skill.name = skillDetail.skillName;
            skill.SetInfo(skillInfo);
            Skills.Add(skill);
        }
        StartCoroutine(UseSkill());
    }


    IEnumerator UseSkill()
    {
        while (true)
        {
            var target = TargetDetector.GetTarget();
            if (target != null && !target.GetComponent<CharacterController>().IsDie)
                foreach (var skill in Skills)
                {
                    if (skill.CheckConditions())
                    {
                        skill.Handle();
                        yield return new WaitForSeconds(4);
                    }
                    yield return null;
                }
            yield return null;
        }
    }

    public override void Death()
    {
        base.Death();
        Observer.Instance.Notify(ConstanVariable.ENEMY_DIE_KEY, this);
        SoundManager.Instance.PlaySound(GameManager.Instance.gameResources.enemyDie);
        ObjectPool.Instance.DestroyObject(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.DrawWireSphere(OriginalPosition, 4);
    }


}



[System.Serializable]
public class EnemyInfo : EntityInfo
{
    public SkillInfoDetails[] skills;

}

[System.Serializable]
public class SpawnEnemyRatio
{
    public string name;
    public int ratio;
}

[System.Serializable]
public class EnemySpawnByLevel
{
    public int enemyCount;
    public int turnCount;
    public SpawnEnemyRatio[] enemies;
}

[System.Serializable]
public class DungeonSpawnEnemyInfo
{
    public int level;
    public EnemySpawnByLevel[] enemySpawnByLevels;
}

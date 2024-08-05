public class DataManager : Singleton<DataManager>
{
    // Character
    public CharacterData CharacterData { private set; get; }
    public StateMachineData CharacterStateMachine { private set; get; }
    public StatDatas CharacterStats { private set; get; }

    // Enemy
    public EnemyData EnemyData { private set; get; }
    public StateMachineData EnemyStateMachine { private set; get; }
    public StatDatas EnemyStats { private set; get; }
    public StatDatas EnemySkillStats { private set; get; }
    public SkillData EnemySkillData { private set; get; }

    // Weapon
    public WeaponData WeaponData { private set; get; }
    public StateMachineData WeaponStateMachine { private set; get; }
    public StatDatas WeaponStats { private set; get; }

    // Other
    public ProjectileData ProjectileData { private set; get; }
    public DetectorData DetectorData { private set; get; }

    public DataManager()
    {
        LoadData();
    }
    public virtual void LoadData()
    {
        CharacterData = new CharacterData();
        CharacterStateMachine = new StateMachineData("CharacterStateMachine");
        CharacterStats = new StatDatas("CharacterStat");

        EnemyData = new EnemyData();
        EnemyStateMachine = new StateMachineData("EnemyStateMachine");
        EnemyStats = new StatDatas("EnemyStat");
        EnemySkillData = new SkillData("EnemySkill");
        EnemySkillStats = new StatDatas("EnemySkillStat");

        WeaponData = new WeaponData();
        WeaponStateMachine = new StateMachineData("WeaponStateMachine");
        WeaponStats = new StatDatas("WeaponStat");

        DetectorData = new DetectorData();
        ProjectileData = new ProjectileData();
    }


}

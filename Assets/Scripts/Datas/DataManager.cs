﻿public class DataManager : Singleton<DataManager>
{
    public StateMachineData WeaponStateMachine { private set; get; }
    public StateMachineData PlayerStateMachine { private set; get; }
    public StateMachineData EnemyStateMachine { private set; get; }
    public WeaponData WeaponDatas { private set; get; }
    public StatDatas WeaponStats { private set; get; }
    public StatDatas PlayerStats { private set; get; }
    public DetectorData DetectorData { private set; get; }
    public PlayerData PlayerData { private set; get; }
    public SkillData SkillData { private set; get; }
    public EnemyData EnemyData { private set; get; }

    public DataManager()
    {
        LoadData();
    }
    public virtual void LoadData()
    {
        WeaponDatas = new WeaponData();
        WeaponStateMachine = new StateMachineData("WeaponStateMachine");
        PlayerStateMachine = new StateMachineData("PlayerStateMachine");
        EnemyStateMachine = new StateMachineData("EnemyStateMachine");
        WeaponStats = new StatDatas("WeaponStat");
        PlayerStats = new StatDatas("PlayerStat");
        DetectorData = new DetectorData();
        PlayerData = new PlayerData();
        SkillData = new SkillData();
        EnemyData = new EnemyData();
    }
}

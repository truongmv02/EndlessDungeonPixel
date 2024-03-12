public class DataManager : Singleton<DataManager>
{
    public StateMachineData WeaponStateMachine { private set; get; }
    public StateMachineData PlayerStateMachine { private set; get; }
    public WeaponData WeaponDatas { private set; get; }
    public StatDatas WeaponStats { private set; get; }
    public StatDatas PlayerStats { private set; get; }

    public DataManager()
    {
        LoadData();
    }
    public virtual void LoadData()
    {
        WeaponDatas = new WeaponData();
        WeaponStateMachine = new StateMachineData("Weapons");
        PlayerStateMachine = new StateMachineData("Players");
        WeaponStats = new StatDatas("Weapons");
        PlayerStats = new StatDatas("Players");
    }
}

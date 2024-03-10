public class DataManager : Singleton<DataManager>
{
    public StateMachineDatas StateMachineDatas { private set; get; }
    public WeaponData WeaponDatas { private set; get; }

    public DataManager()
    {
        LoadData();
    }
    public virtual void LoadData()
    {
        WeaponDatas = new WeaponData();
        StateMachineDatas = new StateMachineDatas();

    }
}

public class DataManager : Singleton<DataManager>
{
    public StateMachineDatas StateMachineDatas { private set; get; }
    public DataManager()
    {
        LoadData();
    }
    public virtual void LoadData()
    {
        //bulletDatas = new BulletDatas();
        StateMachineDatas = new StateMachineDatas();

    }
}

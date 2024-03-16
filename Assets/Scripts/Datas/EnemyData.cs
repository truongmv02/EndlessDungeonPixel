using System.Collections;
using UnityEngine;

public class EnemyData : BaseData
{
    public EnemyData()
    {
        LoadData("Data/Enemies");
    }

    public EnemyInfo GetInfo(string name)
    {
        return GetData<EnemyInfo>(name);
    }
}

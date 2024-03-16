using System.Collections;
using UnityEngine;

public class PlayerData : BaseData
{
    public PlayerData()
    {
        LoadData("Data/Players");
    }

    public PlayerInfo GetInfo(string name)
    {
        return GetData<PlayerInfo>(name);
    }
}

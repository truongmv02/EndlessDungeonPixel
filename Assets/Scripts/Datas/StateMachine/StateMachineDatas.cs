

using UnityEngine;

public class StateMachineDatas : BaseMultiData
{
    public StateMachineDatas()
    {
        LoadData<StateMachineData>("Data/StateMachines");
    }

    public StateMachineInfo GetInfo(string[] paths)
    {
        return ((StateMachineData)Datas[paths[0]])?.GetStateMachineInfo(paths[1]);
    }
}

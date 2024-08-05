using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachineData : StaticData
{
    public StateMachineData(string path)
    {
        LoadData("Data/StateMachines/" + path);
    }

    public StateMachineInfo GetInfo(string name)
    {
        StateMachineInfo stateMachineInfo = GetData<StateMachineInfo>(name);
        AddSubInfos(stateMachineInfo, data_dict[name].AsObject);
        return stateMachineInfo;
    }

    protected void AddSubInfos(StateMachineInfo stateMachineInfo, JSONObject json)
    {
        if (json["states"] != null)
        {
            var jsonArray = json["states"].AsArray;
            stateMachineInfo.states = UtilsData.GetSubInfos(jsonArray);
            for (int i = 0; i < jsonArray.Count; i++)
            {
                var conditionJsonArray = jsonArray[i]["data"]["conditions"];
                if (conditionJsonArray != null)
                    AddSubInfos(stateMachineInfo.states[i].data as StateInfo, conditionJsonArray.AsArray);
            }
        }
    }

    protected void AddSubInfos(StateInfo stateInfo, JSONArray jsonArray)
    {
        stateInfo.conditions = UtilsData.GetSubInfos(jsonArray);
    }

}

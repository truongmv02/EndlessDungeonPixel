using SimpleJSON;
using System.Collections;
using UnityEngine;

public class SkillData : StaticData
{
    public SkillData(string path)
    {
        LoadData("Data/Skills/" + path);
    }

    public SkillInfo GetInfo(string name)
    {
        SkillInfo info = GetData<SkillInfo>(name);
        AddSubInfo(info, data_dict[name].AsObject);
        return info;
    }
    public void AddSubInfo(SkillInfo info, JSONObject json)
    {
        if (json["components"] != null)
        {
            info.components = UtilsData.GetSubInfos(json["components"].AsArray);
        }
        if (json["conditions"] != null)
        {
            info.conditions = UtilsData.GetSubInfos(json["conditions"].AsArray);
        }
        if (json["handles"] != null)
        {
            info.handles = UtilsData.GetSubInfos(json["handles"].AsArray);
        }
    }
}

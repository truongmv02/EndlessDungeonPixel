using SimpleJSON;
using System.Collections;
using UnityEngine;

public class ProjectileData : StaticData
{
    public ProjectileData()
    {
        LoadData("Data/Projectiles");
    }

    public ProjectileInfo GetInfo(string name)
    {
        ProjectileInfo info = GetData<ProjectileInfo>(name);
        var data = GetData(name).AsObject;
        info.hitEffect = Resources.Load<ProjectileHitEffectSO>(data["hitEffect"]);
        info.sprite = UtilsData.GetSprite(data);
        AddSubInfo(info, data);
        return info;
    }

    public void AddSubInfo(ProjectileInfo info, JSONObject json)
    {
        if (json["components"] != null)
        {
            info.components = UtilsData.GetSubInfos(json["components"].AsArray);
        }
    }
}

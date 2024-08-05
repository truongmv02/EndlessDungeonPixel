
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
public class WeaponData : StaticData
{
    public WeaponData()
    {
        LoadData("Data/Weapons");
    }

    public WeaponInfo GetInfo(string weaponName)
    {
        WeaponInfo weaponInfo = GetData<WeaponInfo>(weaponName);
        SetInfo(weaponInfo, GetData(weaponName).AsObject);
        return weaponInfo;
    }

    public List<WeaponInfo> GetAllInfo()
    {
        return GetAllData<WeaponInfo>(SetInfo);
    }


    public void SetInfo(WeaponInfo info, JSONObject obj)
    {
        info.sprite = UtilsData.GetSprite(obj);
        info.itemPrefab = Resources.Load<GameObject>(obj["itemPrefab"]);
        info.sound = Resources.Load<AudioClip>(obj["sound"]);
        info.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(obj["runtimeAnimatorController"]);
        AddSubInfo(info, obj);
    }

    public void AddSubInfo(WeaponInfo info, JSONObject json)
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

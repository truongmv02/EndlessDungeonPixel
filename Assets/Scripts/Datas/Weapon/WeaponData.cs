
using SimpleJSON;
using UnityEngine;
public class WeaponData : BaseData
{
    public WeaponData()
    {
        LoadData("Data/Weapons");
    }

    public WeaponInfo GetInfo(string weaponName)
    {
        WeaponInfo weaponInfo = GetData<WeaponInfo>(weaponName);
        JSONObject data = data_dict[weaponName].AsObject;
        weaponInfo.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(data["runtimeAnimatorController"]);

        AddSubInfo(weaponInfo, data);

        return weaponInfo;
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
    }

}

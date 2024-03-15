
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

        var spriteArray = data["sprite"].AsArray;
        var sprites = new string[spriteArray.Count];
        for (int i = 0; i < spriteArray.Count; i++)
        {
            sprites[i] = spriteArray[i];
        }
        weaponInfo.sprite = BaseUtils.LoadSprite(sprites);
        weaponInfo.itemPrefab = Resources.Load<GameObject>(data["itemPrefab"]);
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

using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : StaticData
{
    public CharacterData()
    {

        LoadData("Data/Characters");
    }

    public CharacterInfo GetInfo(string name)
    {
        var info = GetData<CharacterInfo>(name);
        SetInfo(info, GetData(name).AsObject);
        return info;
    }

    public List<CharacterInfo> GetAllInfo()
    {
        return GetAllData<CharacterInfo>(SetInfo);
    }

    void SetInfo(CharacterInfo info, JSONObject jsonObj)
    {
        info.sprite = UtilsData.GetSprite(jsonObj);
        info.initialWeapon = DataManager.Instance.WeaponData.GetInfo(jsonObj["initialWeapon"]);
    }
}
